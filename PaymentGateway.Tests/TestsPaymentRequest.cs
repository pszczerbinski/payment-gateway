namespace PaymentGateway.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using global::PaymentGateway.Extensions;
    using global::PaymentGateway.Models;
    using Shouldly;

    [TestClass]
    public class TestsPaymentRequest
    {
        [TestMethod]
        public void ThatCanCreateAValidInstance()
        {
            var paymentRequest = TestDataProvider.GetValidPaymentRequest();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.Success);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCardNumber()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithInvalidCardNumber();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCardNumber);
        }

        [TestMethod]
        public void ThatValidationSucceedsWithTodayCardExpiry()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithThisMonthExpiryDate();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.Success);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidExpirationDate()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithInvalidExpiryDate();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidExpirationDate);
        }

        [TestMethod]
        public void ThatValidationFailsWithExpiredCard()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithExpiredCard();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.CardExpired);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCvv()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithInvalidCvv();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCvv);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidAmount()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithInvalidAmount();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidAmount);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCurrency()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithInvalidCurrency();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCurrency);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCardHolderName()
        {
            var paymentRequest = TestDataProvider.GetPaymentRequestWithInvalidCardHolderName();

            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCardHolderName);
        }
    }
}
