namespace PaymentGateway
{
    public enum PaymentRequestValidationResult
    {
        /// <summary> Successful validation. </summary>
        Success = 0,

        /// <summary> Card number is invalid. </summary>
        InvalidCardNumber = 1,

        /// <summary> Card has expired. </summary>
        CardExpired = 2,

        /// <summary> Invalid expiration date. </summary>
        InvalidExpirationDate = 3,

        /// <summary> Invalid cvv value. </summary>
        InvalidCvv = 4,

        /// <summary> Invalid payment amount. </summary>
        InvalidAmount = 5,

        /// <summary> Invalid currency. </summary>
        InvalidCurrency = 6,
    }
}
