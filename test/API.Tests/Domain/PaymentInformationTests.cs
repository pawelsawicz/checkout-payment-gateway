using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Services;
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
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration => registration.Register<IBankComponent, FakeBankComponentWithSuccessfulResponse>())
                .CreateResolver()
            )
            {
                var exampleId = PaymentId.New;
                var commandBus = resolver.Resolve<ICommandBus>();

                await commandBus.PublishAsync(new PayCommand(exampleId), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var result = await queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformation>(exampleId),
                    CancellationToken.None);

                Assert.Equal(typeof(PaymentInformation), result.GetType());
                Assert.NotEmpty(result.bankPaymentResponse.BankIdentifier);
                Assert.NotEmpty(result.bankPaymentResponse.PaymentStatus);
                Assert.Equal("Approved", result.bankPaymentResponse.PaymentStatus);
            }
        }
        
        [Fact]
        public async Task GivenPaymentFailedThenPaymentInformationReadModelCreated()
        {
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration => registration.Register<IBankComponent, FakeBankComponentWithFailedResponse>())
                .CreateResolver()
            )
            {
                var exampleId = PaymentId.New;
                var commandBus = resolver.Resolve<ICommandBus>();

                await commandBus.PublishAsync(new PayCommand(exampleId), CancellationToken.None);

                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var result = await queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformation>(exampleId),
                    CancellationToken.None);

                Assert.Equal(typeof(PaymentInformation), result.GetType());
                Assert.NotEmpty(result.bankPaymentResponse.BankIdentifier);
                Assert.NotEmpty(result.bankPaymentResponse.PaymentStatus);
                Assert.Equal("Failed", result.bankPaymentResponse.PaymentStatus);
            }
        }
    }
}