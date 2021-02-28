namespace PaymentGateway.Tests
{
    using System;
    using System.Threading.Tasks;
    using Bank;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Tests.Shared;
    using Shouldly;

    [TestClass]
    public class TestsPaymentGateway
    {
        private Mock<IBank> bankMock;
        private IPaymentGateway paymentGateway;

        [TestInitialize]
        public void Initialize()
        {
            this.bankMock = new Mock<IBank>();
            this.paymentGateway = new PaymentGateway(this.bankMock.Object);
        }

        [TestMethod]
        public void ThatThrowsWithNullPaymentRequest()
        {
            PaymentRequest paymentRequest = null;

            Should.Throw<ArgumentNullException>(
                () => this.paymentGateway.ProcessPaymentRequestAsync(paymentRequest));
        }

        [TestMethod]
        public async Task ThatCanProcessSuccessfulPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();

            var expectedResponse = new BankPaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            this.bankMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<BankPaymentRequest>())).ReturnsAsync(expectedResponse);

            var actualResponse = await this.paymentGateway.ProcessPaymentRequestAsync(request);

            actualResponse.Identifier.ShouldBe(expectedResponse.Identifier);
            actualResponse.Success.ShouldBe(expectedResponse.Success);
            actualResponse.Error.ShouldBe(expectedResponse.Error);
        }

        [TestMethod]
        public async Task ThatCanProcessFailedPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();

            var expectedResponse = new BankPaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            this.bankMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<BankPaymentRequest>())).ReturnsAsync(expectedResponse);

            var actualResponse = await this.paymentGateway.ProcessPaymentRequestAsync(request);

            actualResponse.Identifier.ShouldBe(expectedResponse.Identifier);
            actualResponse.Success.ShouldBe(expectedResponse.Success);
            actualResponse.Error.ShouldBe(expectedResponse.Error);
        }
    }
}
