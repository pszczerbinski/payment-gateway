namespace PaymentGateway.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Tests.Shared;
    using Shouldly;

    [TestClass]
    public class TestsPaymentGateway
    {
        private PaymentGatewayApi paymentGateway;

        [TestInitialize]
        public void Initialize()
        {
            var address = IntegrationTestSettings.Address;
            this.paymentGateway = new PaymentGatewayApi(address);
        }

        [TestMethod]
        public async Task ThatCannotProcessNullPayment()
        {
            PaymentRequest request = null;

            await Should.ThrowAsync<ArgumentNullException>(
                () => this.paymentGateway.ProcessPaymentAsync(request));
        }

        [TestMethod]
        public async Task ThatCanProcessValidPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Error.ShouldBeNull();
        }

        [TestMethod]
        public async Task ThatPaymentInEuroIsUnsuccessful()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            request.Currency = Currency.EURO;

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe("Unsupported currency");
        }

        [TestMethod]
        public async Task ThatPaymentOfOver1000PoundsIsUnsuccessful()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            request.Amount = 5000;

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe("Insufficient funds");
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithInvalidCardNumber()
        {
            var request = TestDataProvider.GetPaymentRequestWithInvalidCardNumber();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.InvalidCardNumber.ToString());
        }

        [TestMethod]
        public async Task ThatCanProcessPaymentWithThisMonthCardExpiry()
        {
            var request = TestDataProvider.GetPaymentRequestWithThisMonthExpiryDate();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithInvalidExpirationDate()
        {
            var request = TestDataProvider.GetPaymentRequestWithInvalidExpiryDate();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.InvalidExpirationDate.ToString());
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithExpiredCard()
        {
            var request = TestDataProvider.GetPaymentRequestWithExpiredCard();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.CardExpired.ToString());
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithInvalidCvv()
        {
            var request = TestDataProvider.GetPaymentRequestWithInvalidCvv();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.InvalidCvv.ToString());
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithInvalidAmount()
        {
            var request = TestDataProvider.GetPaymentRequestWithInvalidAmount();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.InvalidAmount.ToString());
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithInvalidCurrency()
        {
            var request = TestDataProvider.GetPaymentRequestWithInvalidCurrency();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.InvalidCurrency.ToString());
        }

        [TestMethod]
        public async Task ThatCannotProcessPaymentWithInvalidCardHolderName()
        {
            var request = TestDataProvider.GetPaymentRequestWithInvalidCardHolderName();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);

            result.ShouldNotBeNull();
            result.Identifier.ShouldBeNull();
            result.Success.ShouldBeFalse();
            result.Error.ShouldBe(PaymentRequestValidationResult.InvalidCardHolderName.ToString());
        }

        [TestMethod]
        public async Task ThatCanRetrieveValidPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();

            var result = await this.paymentGateway.ProcessPaymentAsync(request);
            result.ShouldNotBeNull();

            var details = await this.paymentGateway.GetPaymentDetailsAsync(result.Identifier);
            details.ShouldNotBeNull();
            details.Identifier.ShouldBe(result.Identifier);
            details.Success.ShouldBe(result.Success);
            details.Error.ShouldBe(result.Error);
            details.MaskedCardNumber[12..].ShouldBe(request.CardNumber[12..]);
        }

        [TestMethod]
        public async Task ThatCanRetrieveFailedPayment()
        {
            var request = TestDataProvider.GetValidPaymentRequest();
            request.Amount = 5000;

            var result = await this.paymentGateway.ProcessPaymentAsync(request);
            result.ShouldNotBeNull();

            var details = await this.paymentGateway.GetPaymentDetailsAsync(result.Identifier);
            details.ShouldNotBeNull();
            details.Identifier.ShouldBe(result.Identifier);
            details.Success.ShouldBe(result.Success);
            details.Error.ShouldBe(result.Error);
            details.MaskedCardNumber[12..].ShouldBe(request.CardNumber[12..]);
        }
    }
}
