namespace PaymentGateway.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using global::PaymentGateway.Controllers;
    using global::PaymentGateway.Models;
    using Shouldly;
    using Microsoft.AspNetCore.Mvc;

    [TestClass]
    public class TestsPaymentGatewayController
    {
        private Mock<IPaymentGateway> paymentGatewayMock;
        private PaymentGatewayController controller;

        [TestInitialize]
        public void Initialize()
        {
            this.paymentGatewayMock = new Mock<IPaymentGateway>();
            this.controller = new PaymentGatewayController(this.paymentGatewayMock.Object);
        }

        [TestMethod]
        public async Task ThatPostProcessPaymentReturnsOkResponse()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var expectedResponse = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            this.paymentGatewayMock.Setup(x => x.ProcessPaymentRequest(It.IsAny<PaymentRequest>())).ReturnsAsync(expectedResponse);

            var response = await this.controller.PostProcessPayment(request);

            response.Result.ShouldBeOfType<OkObjectResult>();
            var okResponse = response.Result as OkObjectResult;
            okResponse.Value.ShouldBeOfType<PaymentResponse>();
            var actualReponse = okResponse.Value as PaymentResponse;
            actualReponse.Identifier.ShouldBe(expectedResponse.Identifier);
            actualReponse.Success.ShouldBeTrue();
            actualReponse.Error.ShouldBeNull();
        }

        [TestMethod]
        public async Task ThatPostProcessPaymentReturnsBadRequestWithNullPaymentRequest()
        {
            PaymentRequest request = null;

            var response = await this.controller.PostProcessPayment(request);

            response.Result.ShouldBeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task ThatPostProcessPaymentReturnsBadRequestWithInvalidPaymentRequest()
        {
            var request = TestDataProvider.GetPaymentRequestWithExpiredCard();

            var response = await this.controller.PostProcessPayment(request);

            response.Result.ShouldBeOfType<BadRequestObjectResult>();
            var badRequestResponse = response.Result as BadRequestObjectResult;
            badRequestResponse.Value.ShouldBeOfType<PaymentResponse>();
            var paymentReponse = badRequestResponse.Value as PaymentResponse;
            paymentReponse.Identifier.ShouldBeNull();
            paymentReponse.Success.ShouldBeFalse();
            paymentReponse.Error.ShouldBe(PaymentRequestValidationResult.CardExpired.ToString());
        }
    }
}
