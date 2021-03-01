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
        private Mock<IPaymentStorage> paymentStorageMock;
        private IPaymentGateway paymentGateway;

        [TestInitialize]
        public void Initialize()
        {
            this.bankMock = new Mock<IBank>();
            this.paymentStorageMock = new Mock<IPaymentStorage>();
            this.paymentGateway = new PaymentGateway(this.bankMock.Object, this.paymentStorageMock.Object);
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
            this.paymentStorageMock.Setup(x => x.SavePaymentDetails(It.IsAny<PaymentRequest>(), It.IsAny<PaymentResponse>())).Returns(true);

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
                Error = "Payment failed",
            };

            this.bankMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<BankPaymentRequest>())).ReturnsAsync(expectedResponse);
            this.paymentStorageMock.Setup(x => x.SavePaymentDetails(It.IsAny<PaymentRequest>(), It.IsAny<PaymentResponse>())).Returns(true);

            var actualResponse = await this.paymentGateway.ProcessPaymentRequestAsync(request);

            actualResponse.Identifier.ShouldBe(expectedResponse.Identifier);
            actualResponse.Success.ShouldBe(expectedResponse.Success);
            actualResponse.Error.ShouldBe(expectedResponse.Error);
        }

        [TestMethod]
        public async Task ThatCanRetrieveASuccessfulPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();

            var expectedBankResponse = new BankPaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            var expectedPaymentDetails = new PaymentDetails
            {
                Identifier = expectedBankResponse.Identifier,
                MaskedCardNumber = $"************{request.CardNumber[12..]}",
            };

            this.bankMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<BankPaymentRequest>())).ReturnsAsync(expectedBankResponse);
            this.paymentStorageMock.Setup(x => x.SavePaymentDetails(It.IsAny<PaymentRequest>(), It.IsAny<PaymentResponse>())).Returns(true);
            this.paymentStorageMock.Setup(x => x.RetrievePaymentDetails(It.IsAny<string>())).Returns(expectedPaymentDetails);

            var paymentResponse = await this.paymentGateway.ProcessPaymentRequestAsync(request);
            var actualPaymentDetails = this.paymentGateway.RetrievePaymentDetails(paymentResponse.Identifier);

            actualPaymentDetails.ShouldNotBeNull();
            actualPaymentDetails.Identifier.ShouldBe(expectedPaymentDetails.Identifier);
            actualPaymentDetails.Success.ShouldBe(expectedPaymentDetails.Success);
            actualPaymentDetails.Error.ShouldBe(expectedPaymentDetails.Error);
            actualPaymentDetails.MaskedCardNumber.ShouldBe(expectedPaymentDetails.MaskedCardNumber);
        }

        [TestMethod]
        public async Task ThatCanRetrieveAFailedPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();

            var expectedBankResponse = new BankPaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
                Error = "Payment failed",
            };

            var expectedPaymentDetails = new PaymentDetails
            {
                Identifier = expectedBankResponse.Identifier,
                MaskedCardNumber = $"************{request.CardNumber[12..]}",
            };

            this.bankMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<BankPaymentRequest>())).ReturnsAsync(expectedBankResponse);
            this.paymentStorageMock.Setup(x => x.SavePaymentDetails(It.IsAny<PaymentRequest>(), It.IsAny<PaymentResponse>())).Returns(true);
            this.paymentStorageMock.Setup(x => x.RetrievePaymentDetails(It.IsAny<string>())).Returns(expectedPaymentDetails);

            var paymentResponse = await this.paymentGateway.ProcessPaymentRequestAsync(request);
            var actualPaymentDetails = this.paymentGateway.RetrievePaymentDetails(paymentResponse.Identifier);

            actualPaymentDetails.ShouldNotBeNull();
            actualPaymentDetails.Identifier.ShouldBe(expectedPaymentDetails.Identifier);
            actualPaymentDetails.Success.ShouldBe(expectedPaymentDetails.Success);
            actualPaymentDetails.Error.ShouldBe(expectedPaymentDetails.Error);
            actualPaymentDetails.MaskedCardNumber.ShouldBe(expectedPaymentDetails.MaskedCardNumber);
        }
    }
}
