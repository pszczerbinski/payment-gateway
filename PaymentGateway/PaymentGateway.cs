namespace PaymentGateway
{
    using System;
    using System.Threading.Tasks;
    using Bank;
    using global::PaymentGateway.Converters;
    using global::PaymentGateway.Models;

    /// <summary>
    /// Implementation of the payment gateway.
    /// </summary>
    public class PaymentGateway : IPaymentGateway
    {
        private readonly IBank bank;
        private readonly IPaymentStorage storage;

        public PaymentGateway(IBank bank, IPaymentStorage storage)
        {
            this.bank = bank ?? throw new ArgumentNullException(nameof(bank));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async Task<PaymentResponse> ProcessPaymentRequestAsync(PaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var bankRequest = BankModelConverter.From(request);
            var banksResponse = await this.bank.ProcessPaymentAsync(bankRequest);
            var response = BankModelConverter.To(banksResponse);
            this.storage.SavePaymentDetails(request, response);
            return response;
        }

        public PaymentDetails RetrievePaymentDetails(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            return this.storage.RetrievePaymentDetails(identifier);
        }
    }
}
