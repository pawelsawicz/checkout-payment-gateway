using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace API.Tests.Controllers
{
    public class PaymentsControllerTests
    {
        private readonly PaymentsController sut;
        
//        public PaymentsControllerTests()
//        {
//            sut = new PaymentsController()
//        }
//        
//        [Fact]
//        public async Task Get()
//        {
//            var result = await sut.Get(5);
//
//            var okObjectResult = result.ShouldBeAssignableTo<OkObjectResult>();
//            okObjectResult.Value.ShouldBe(5);
//        }
//
//        [Fact]
//        public async Task Post()
//        {
//            var exampleRequest = new PaymentRequest();
//            
//            var result = await sut.Post(exampleRequest);
//            
//            var okObjectResult = result.ShouldBeAssignableTo<OkObjectResult>();
//            okObjectResult.Value.ShouldBe(20);
//        }
    }
}