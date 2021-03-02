namespace PaymentGateway.Stubs
{
    using System;
    using System.Threading.Tasks;
    using Bank;

    /// <summary>
    /// Simple implementation of <see cref="IBank"/>.
    /// </summary>
    public class BankStub : IBank
    {
        private const double AmountLimit = 1000;

        public Task<BankPaymentResponse> ProcessPaymentAsync(BankPaymentRequest request)
        {
            if (!request.Currency.Equals("GBP"))
            {
                return Task.FromResult(new BankPaymentResponse
                {
                    Identifier = Guid.NewGuid().ToString(),
                    Success = false,
                    Error = "Unsupported currency",
                });
            }

            if (request.Amount > AmountLimit)
            {
                return Task.FromResult(new BankPaymentResponse
                {
                    Identifier = Guid.NewGuid().ToString(),
                    Success = false,
                    Error = "Insufficient funds",
                });
            }
            else if (request.Amount >= 0)
            {
                return Task.FromResult(new BankPaymentResponse
                {
                    Identifier = Guid.NewGuid().ToString(),
                    Success = true,
                });
            }

            return Task.FromResult(new BankPaymentResponse
            {
                Identifier = Guid.NewGuid().ToString(),
                Success = false,
                Error = "Invalid amount",
            });
        }
    }
}
