namespace PaymentGateway.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PaymentGateway.Extensions;
    using PaymentGateway.Models;
    using Shouldly;

    [TestClass]
    public class TestsPaymentRequest
    {
        [TestMethod]
        public void ThatCanCreateAValidInstance()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy");
            var cvv = "123";
            var amount = 1.99;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.Success);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCardNumber()
        {
            var cardNumber = "123412341234123";
            var expirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy");
            var cvv = "123";
            var amount = 1.99;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCardNumber);
        }

        [TestMethod]
        public void ThatValidationSucceedsWithTodayCardExpiry()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = DateTime.Today.ToString("MM/yyyy");
            var cvv = "123";
            var amount = 1.99;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.Success);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidExpirationDate()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = "12-9999";
            var cvv = "123";
            var amount = 1.99;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidExpirationDate);
        }

        [TestMethod]
        public void ThatValidationFailsWithExpiredCard()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = (DateTime.Now - TimeSpan.FromDays(31)).ToString("MM/yyyy");
            var cvv = "123";
            var amount = 1.99;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.CardExpired);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCvv()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy");
            var cvv = "123456";
            var amount = 1.99;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCvv);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidAmount()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy");
            var cvv = "123";
            var amount = -1;
            var currency = Currency.GBP;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidAmount);
        }

        [TestMethod]
        public void ThatValidationFailsWithInvalidCurrency()
        {
            var cardNumber = "1234123412341234";
            var expirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy");
            var cvv = "123";
            var amount = 1.99;
            Currency currency = null;

            var paymentRequest = new PaymentRequest
            {
                CardNumber = cardNumber,
                CardExpirationDate = expirationDate,
                CardVerificationValue = cvv,
                Amount = amount,
                Currency = currency
            };
            var validationResult = paymentRequest.Validate();

            validationResult.ShouldBe(PaymentRequestValidationResult.InvalidCurrency);
        }
    }
}
