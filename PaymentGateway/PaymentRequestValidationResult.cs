namespace PaymentGateway
{
    public enum PaymentRequestValidationResult
    {
        Success = 0,
        InvalidCardNumber = 1,
        CardExpired = 2,
        InvalidExpirationDate = 3,
        InvalidCvv = 4,
        InvalidAmount = 5,
        InvalidCurrency = 6,
    }
}
