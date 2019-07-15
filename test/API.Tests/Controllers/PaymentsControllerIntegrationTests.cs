using System;
using System.Threading.Tasks;
using API.Controllers;
using API.Domain;
using API.Domain.Events;
using API.Models;
using API.Services;
using API.Services.FakeAcquiringBankImpls;
using CreditCardValidator;
using EventFlow;
using EventFlow.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace API.Tests.Controllers
{
    public class PaymentsControllerIntegrationTests
    {
        private readonly PaymentsController sut;
        
        public PaymentsControllerIntegrationTests()
        {
            var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded), typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformationReadModel>()
                .RegisterServices(registration =>
                    registration.Register<IAcquiringBankService>(x => new FakeAcquiringBankServiceWithSuccessfulResponse()))
                .CreateResolver();
            sut = new PaymentsController(resolver);
        }
        
        [Fact]
        public async Task GivenValidPaymentIdWhenGetThenReturns200StatusCodeWithPayment()
        {
            //arrange
            var paymentRequest = CreateValidRequest();
            var postResult = await sut.Post(paymentRequest);
            var created = postResult.ShouldBeAssignableTo<CreatedResult>()
                .Value.ShouldBeAssignableTo<PaymentInformationReadModel>();

            // act
            var getResult = await sut.Get(created.PaymentId);

            // assert
            getResult.ShouldBeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async Task GivenValidDataWhenPostThenReturns201StatusCodeWithCreatedPayment()
        {
            // arrange
            var paymentRequest = CreateValidRequest();
            
            // act
            var result = await sut.Post(paymentRequest);
            
            // assert
            var created = result.ShouldBeAssignableTo<CreatedResult>();
            var returnedBody = created.Value.ShouldBeAssignableTo<PaymentInformationReadModel>();
            
            created.Location.ShouldNotBeEmpty();
            returnedBody.ShouldSatisfyAllConditions(
                () => returnedBody.PaymentId.ShouldNotBeEmpty(),
                () => returnedBody.PaymentStatus.CardNumber.ShouldBe(Mask(paymentRequest.CardNumber)),
                () => returnedBody.PaymentStatus.ExpiryMonth.ShouldBe(paymentRequest.ExpiryMonth),
                () => returnedBody.PaymentStatus.ExpiryDate.ShouldBe(paymentRequest.ExpiryDate),
                () => returnedBody.PaymentStatus.Name.ShouldBe(paymentRequest.Name),
                () => returnedBody.PaymentStatus.Amount.ShouldBe(paymentRequest.Amount),
                () => returnedBody.PaymentStatus.Currency.ShouldBe(paymentRequest.CurrencyCode),

                () => returnedBody.PaymentStatus.PaymentStatusCode.ShouldBe(
                    FakeAcquiringBankServiceWithSuccessfulResponse.ReturnedStatusCode),
                () => returnedBody.PaymentStatus.BankIdentifier.ShouldNotBeEmpty(),
                () => returnedBody.Links.self_href.ShouldBe(created.Location));
        }

        [Fact]
        public async Task GivenInvalidDataWhenPostThenReturns400StatusCode()
        {
            // arrange
            var paymentRequest = CreateInvalidRequest();
            sut.ModelState.AddModelError("Request", "Invalid");
            
            // act
            var result = await sut.Post(paymentRequest);
            
            // assert
            result.ShouldBeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GivenInvalidPaymentWhenGetThenReturns404StatusCode()
        {
            var getResult = await sut.Get(Guid.NewGuid().ToString());

            
            getResult.ShouldBeAssignableTo<NotFoundResult>();
        }
        
        private string Mask(string cardNumber)
        {
            var countToMask = cardNumber.Length - 4;
            var firstPart = new String('*', countToMask);
            var lastPart = cardNumber.Substring(countToMask, 4);
            return firstPart + lastPart;
        }

        private PaymentRequest CreateValidRequest() => new PaymentRequest
        {
            CardNumber = CreditCardFactory.RandomCardNumber(CardIssuer.Visa),
            ExpiryMonth = 8,
            ExpiryDate = 2019,
            Amount = 2000,
            Name = "Alfred Tarski",
            CurrencyCode = "USD",
            Cvv = 966
        };
        
        private PaymentRequest CreateInvalidRequest() => new PaymentRequest
        {
            CardNumber = CreditCardFactory.RandomCardNumber(CardIssuer.Visa),
            ExpiryMonth = 8,
            ExpiryDate = 2016,
            Amount = 0,
            Name = "Alfred Tarski",
            CurrencyCode = "USDD",
            Cvv = 96
        };
    }
}