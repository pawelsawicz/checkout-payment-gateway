using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Commands;
using API.Domain.Events;
using API.Services;
using API.Services.FakeAcquiringBankImpls;
using CreditCardValidator;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using Shouldly;
using Xunit;

namespace API.Tests.Domain
{
    public class PaymentInformationTests
    {
        [Fact]
        public async Task GivenPaymentSucceededThenPaymentInformationReadModelIsCreated()
        {
            // arrange
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformationReadModel>()
                .RegisterServices(registration => 
                    registration.Register<IAcquiringBankService, FakeAcquiringBankServiceWithSuccessfulResponse>())
                .CreateResolver()
            )
            {
                var paymentId = PaymentId.New;
                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var commandBus = resolver.Resolve<ICommandBus>();
                var bankPaymentRequest = CreateRequest();
                await commandBus.PublishAsync(new PayCommand(paymentId, bankPaymentRequest), CancellationToken.None);

                // act
                var result = await queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformationReadModel>(paymentId),
                    CancellationToken.None);

                // assert
                var expectedMaskedCardNumber = Mask(bankPaymentRequest.CardNumber);

                result.ShouldSatisfyAllConditions(
                    () => result.PaymentStatus.BankIdentifier.ShouldNotBeEmpty(),
                    () => result.PaymentStatus.PaymentStatusCode.ShouldBe(
                        FakeAcquiringBankServiceWithSuccessfulResponse.ReturnedStatusCode),
                    () => result.PaymentStatus.CardNumber.ShouldBe(expectedMaskedCardNumber),
                    () => result.PaymentStatus.ExpiryMonth.ShouldBe(bankPaymentRequest.ExpiryMonth),
                    () => result.PaymentStatus.ExpiryDate.ShouldBe(bankPaymentRequest.ExpiryDate),
                    () => result.PaymentStatus.Name.ShouldBe(bankPaymentRequest.Name),
                    () => result.PaymentStatus.Amount.ShouldBe(bankPaymentRequest.Amount),
                    () => result.PaymentStatus.Currency.ShouldBe(bankPaymentRequest.CurrencyCode),
                    () => result.Links.self_href.ShouldBe($"http://localhost:5000/api/payments/{paymentId.Value}")
                );
            }
        }
        
        [Fact]
        public async Task GivenPaymentFailedThenPaymentInformationReadModelIsCreated()
        {
            // arrange
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformationReadModel>()
                .RegisterServices(registration => 
                    registration.Register<IAcquiringBankService, FakeAcquiringBankServiceWithFailedResponse>())
                .CreateResolver()
            )
            {
                var paymentId = PaymentId.New;
                var commandBus = resolver.Resolve<ICommandBus>();
                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var bankPaymentRequest = CreateRequest();
                await commandBus.PublishAsync(new PayCommand(paymentId, bankPaymentRequest), CancellationToken.None);
                
                
                // act
                var result = await queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformationReadModel>(paymentId),
                    CancellationToken.None);

                // assert
                var expectedMaskedCardNumber = Mask(bankPaymentRequest.CardNumber);
                result.ShouldSatisfyAllConditions(
                    () => result.PaymentStatus.BankIdentifier.ShouldNotBeEmpty(),
                    () => result.PaymentStatus.PaymentStatusCode.ShouldBe(
                        FakeAcquiringBankServiceWithFailedResponse.ReturnedStatusCode),
                    () => result.PaymentStatus.CardNumber.ShouldBe(expectedMaskedCardNumber),
                    () => result.PaymentStatus.ExpiryMonth.ShouldBe(bankPaymentRequest.ExpiryMonth),
                    () => result.PaymentStatus.ExpiryDate.ShouldBe(bankPaymentRequest.ExpiryDate),
                    () => result.PaymentStatus.Name.ShouldBe(bankPaymentRequest.Name),
                    () => result.PaymentStatus.Amount.ShouldBe(bankPaymentRequest.Amount),
                    () => result.PaymentStatus.Currency.ShouldBe(bankPaymentRequest.CurrencyCode),
                    () => result.Links.self_href.ShouldBe($"http://localhost:5000/api/payments/{paymentId.Value}")
                );
            }
        }

        private AcquiringBankPaymentRequest CreateRequest() => new AcquiringBankPaymentRequest(
            CreditCardFactory.RandomCardNumber(CardIssuer.Visa),
            8,
            2019,
            "Alfred Tarski",
            2000,
            "USD",
            966
        );
        
        private string Mask(string cardNumber)
        {
            var countToMask = cardNumber.Length - 4;
            var firstPart = new String('*', countToMask);
            var lastPart = cardNumber.Substring(countToMask, 4);
            return firstPart + lastPart;
        }
    }
}