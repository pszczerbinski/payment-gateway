namespace PaymentGateway.Tests
{
    using System;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Tests.Shared;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class TestsPaymentStorage
    {
        private PaymentStorage paymentStorage;

        [TestInitialize]
        public void Initialize()
        {
            this.paymentStorage = new PaymentStorage();
        }

        [TestMethod]
        public void ThatSavePaymentThrowsWithNullRequest()
        {
            PaymentRequest request = null;
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            Should.Throw<ArgumentNullException>(() => this.paymentStorage.SavePaymentDetails(request, response));
        }

        [TestMethod]
        public void ThatSavePaymentThrowsWithNullResponse()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            PaymentResponse response = null;

            Should.Throw<ArgumentNullException>(() => this.paymentStorage.SavePaymentDetails(request, response));
        }

        [TestMethod]
        public void ThatCanSavePayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            var result = this.paymentStorage.SavePaymentDetails(request, response);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void ThatGetPaymentDetailsThrowsWithNullIdentifier()
        {
            string identifier = null;

            Should.Throw<ArgumentNullException>(() => this.paymentStorage.RetrievePaymentDetails(identifier));
        }

        [TestMethod]
        public void ThatCanRetrievePaymentDetails()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            var result = this.paymentStorage.SavePaymentDetails(request, response);
            var details = this.paymentStorage.RetrievePaymentDetails(response.Identifier);

            details.ShouldNotBeNull();
            details.Identifier.ShouldBe(response.Identifier);
            details.Success.ShouldBe(response.Success);
            details.Error.ShouldBe(response.Error);
            details.MaskedCardNumber[12..].ShouldBe(request.CardNumber[12..]);
        }

        [TestMethod]
        public void ThatCannotRetrievePaymentDetailsWithInvalidIdentifier()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            var response = new PaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
            };

            _ = this.paymentStorage.SavePaymentDetails(request, response);
            var details = this.paymentStorage.RetrievePaymentDetails(Guid.NewGuid().ToString());

            details.ShouldBeNull();
        }
    }
}
