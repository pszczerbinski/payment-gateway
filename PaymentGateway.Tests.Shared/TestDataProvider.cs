namespace PaymentGateway.Tests.Shared
{
    using System;
    using global::PaymentGateway.Models;

    public static class TestDataProvider
    {
        public static PaymentRequest GetValidPaymentRequest()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithInvalidCardNumber()
        {
            return new PaymentRequest
            {
                CardNumber = "123412341234123",
                CardExpirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithThisMonthExpiryDate()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = DateTime.Today.ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithInvalidExpiryDate()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = "12-9999",
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithExpiredCard()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = (DateTime.Now - TimeSpan.FromDays(31)).ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithInvalidCvv()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy"),
                CardVerificationValue = "123456",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithInvalidAmount()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = -1,
                Currency = Currency.GBP,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithInvalidCurrency()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = null,
                CardHolderName = "Patryk Szczerbinski",
            };
        }

        public static PaymentRequest GetPaymentRequestWithInvalidCardHolderName()
        {
            return new PaymentRequest
            {
                CardNumber = "1234123412341234",
                CardExpirationDate = (DateTime.Now + TimeSpan.FromDays(30)).ToString("MM/yyyy"),
                CardVerificationValue = "123",
                Amount = 1.99,
                Currency = Currency.GBP,
                CardHolderName = null,
            };
        }
    }
}
