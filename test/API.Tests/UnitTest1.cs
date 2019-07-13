using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using Xunit;

namespace API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GivenPayCommandThenPaymentInformationCreated()
        {
            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration => registration.Register<IBankComponent, FakeBankComponent>())
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
            }
        }
    }
}
