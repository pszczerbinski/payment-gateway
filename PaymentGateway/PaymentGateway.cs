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

        public PaymentGateway(IBank bank)
        {
            this.bank = bank ?? throw new ArgumentNullException(nameof(bank));
        }

        public async Task<PaymentResponse> ProcessPaymentRequest(PaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var bankRequest = BankModelConverter.From(request);
            var response = await this.bank.ProcessPayment(bankRequest);
            return BankModelConverter.To(response);
        }
    }
}
