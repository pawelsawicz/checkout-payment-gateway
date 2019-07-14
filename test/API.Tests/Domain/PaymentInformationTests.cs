using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Commands;
using API.Domain.Events;
using API.Services;
using API.Services.FakeAcquiringBankImpls;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using Xunit;

namespace API.Tests.Domain
{
    public class PaymentInformationTests
    {
        [Fact]
        public async Task GivenPaymentSucceededThenPaymentInformationReadModelCreated()
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
                Assert.NotEmpty(result.PaymentStatus.BankIdentifier);
                Assert.Equal("Approved", result.PaymentStatus.PaymentStatusCode);
                Assert.Equal(bankPaymentRequest.CardNumber, result.PaymentStatus.CardNumber);
                Assert.Equal(bankPaymentRequest.ExpiryMonth, result.PaymentStatus.ExpiryMonth);
                Assert.Equal(bankPaymentRequest.ExpiryDate, result.PaymentStatus.ExpiryDate);
                Assert.Equal(bankPaymentRequest.Name, result.PaymentStatus.Name);
                Assert.Equal(bankPaymentRequest.Amount, result.PaymentStatus.Amount);
                Assert.Equal(bankPaymentRequest.CurrencyCode, result.PaymentStatus.CurrencyCode);
                Assert.Equal($"http://localhost:5000/api/payments/{paymentId.Value}", result.Links.self_href);
            }
        }
        
        [Fact]
        public async Task GivenPaymentFailedThenPaymentInformationReadModelCreated()
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
                Assert.NotEmpty(result.PaymentStatus.BankIdentifier);
                Assert.Equal("Failed", result.PaymentStatus.PaymentStatusCode);
                Assert.Equal(bankPaymentRequest.CardNumber, result.PaymentStatus.CardNumber);
                Assert.Equal(bankPaymentRequest.ExpiryMonth, result.PaymentStatus.ExpiryMonth);
                Assert.Equal(bankPaymentRequest.ExpiryDate, result.PaymentStatus.ExpiryDate);
                Assert.Equal(bankPaymentRequest.Name, result.PaymentStatus.Name);
                Assert.Equal(bankPaymentRequest.Amount, result.PaymentStatus.Amount);
                Assert.Equal(bankPaymentRequest.CurrencyCode, result.PaymentStatus.CurrencyCode);
                Assert.Equal($"http://localhost:5000/api/payments/{paymentId.Value}", result.Links.self_href);
            }
        }

        private AcquiringBankPaymentRequest CreateRequest() => new AcquiringBankPaymentRequest
        {
            CardNumber = Guid.NewGuid().ToString(),
            ExpiryMonth = 8,
            ExpiryDate = 2019,
            Amount = 2000,
            Name = "Alfred Tarski",
            CurrencyCode = "USD",
            Cvv = 966
        };
    }
}