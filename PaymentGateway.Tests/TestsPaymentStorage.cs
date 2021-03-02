namespace PaymentGateway.Tests
{
    using System;
    using System.Threading.Tasks;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Storage;
    using global::PaymentGateway.Tests.Shared;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class TestsPaymentStorage
    {
        private PaymentStorage paymentStorage;

        [TestInitialize]
        public void Initialize()
        {
            this.paymentStorage = new PaymentStorage(NullLoggerFactory.Instance);
        }

        [TestMethod]
        public void ThatSavePaymentThrowsWithNullRequest()
        {
            PaymentRequest request = null;
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
                Success = true,
            };

            Should.Throw<ArgumentNullException>(() => this.paymentStorage.SavePaymentDetailsAsync(request, response));
        }

        [TestMethod]
        public void ThatSavePaymentThrowsWithNullResponse()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            PaymentResponse response = null;

            Should.Throw<ArgumentNullException>(() => this.paymentStorage.SavePaymentDetailsAsync(request, response));
        }

        [TestMethod]
        public async Task ThatCanSavePayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
                Success = true,
            };

            var result = await this.paymentStorage.SavePaymentDetailsAsync(request, response);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void ThatGetPaymentDetailsThrowsWithNullIdentifier()
        {
            string identifier = null;

            Should.Throw<ArgumentNullException>(() => this.paymentStorage.RetrievePaymentDetailsAsync(identifier));
        }

        [TestMethod]
        public async Task ThatCanRetrievePaymentDetails()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
                Success = true,
            };

            var result = await this.paymentStorage.SavePaymentDetailsAsync(request, response);
            var details = await this.paymentStorage.RetrievePaymentDetailsAsync(response.Identifier);

            details.ShouldNotBeNull();
            details.Identifier.ShouldBe(response.Identifier);
            details.Success.ShouldBe(response.Success);
            details.Error.ShouldBe(response.Error);
            details.MaskedCardNumber[12..].ShouldBe(request.CardNumber[12..]);
            details.Amount.ShouldBe(request.Amount);
            details.Currency.ShouldBe(request.Currency.Code);
        }

        [TestMethod]
        public async Task ThatCannotRetrievePaymentDetailsWithInvalidIdentifier()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
                Success = true,
            };

            _ = await this.paymentStorage.SavePaymentDetailsAsync(request, response);
            var details = this.paymentStorage.RetrievePaymentDetailsAsync(Guid.NewGuid().ToString());

            details.ShouldBeNull();
        }
    }
}
