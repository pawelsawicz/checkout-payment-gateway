using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
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
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration => 
                    registration.Register<IAcquiringBankService, FakeAcquiringBankServiceWithSuccessfulResponse>())
                .CreateResolver()
            )
            {
                var exampleId = PaymentId.New;
                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var commandBus = resolver.Resolve<ICommandBus>();
                await commandBus.PublishAsync(new PayCommand(exampleId, CreateRequest()), CancellationToken.None);

                // act
                var result = await queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformation>(exampleId),
                    CancellationToken.None);

                // assert
                Assert.Equal(typeof(PaymentInformation), result.GetType());
                Assert.NotEmpty(result.bankPaymentResponse.BankIdentifier);
                Assert.NotEmpty(result.bankPaymentResponse.PaymentStatus);
                Assert.Equal("Approved", result.bankPaymentResponse.PaymentStatus);
                Assert.Equal($"http://localhost:5000/api/payments/{exampleId.Value}", result.Links.self_href);
            }
        }
        
        [Fact]
        public async Task GivenPaymentFailedThenPaymentInformationReadModelCreated()
        {
            // arrange
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration => 
                    registration.Register<IAcquiringBankService, FakeAcquiringBankServiceWithFailedResponse>())
                .CreateResolver()
            )
            {
                var exampleId = PaymentId.New;
                var commandBus = resolver.Resolve<ICommandBus>();
                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                await commandBus.PublishAsync(new PayCommand(exampleId, CreateRequest()), CancellationToken.None);
                
                
                // act
                var result = await queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformation>(exampleId),
                    CancellationToken.None);

                // assert
                Assert.Equal(typeof(PaymentInformation), result.GetType());
                Assert.NotEmpty(result.bankPaymentResponse.BankIdentifier);
                Assert.NotEmpty(result.bankPaymentResponse.PaymentStatus);
                Assert.Equal("Failed", result.bankPaymentResponse.PaymentStatus);
                Assert.Equal($"http://localhost:5000/api/payments/{exampleId.Value}", result.Links.self_href);
            }
        }

        private BankPaymentRequest CreateRequest() => new BankPaymentRequest
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