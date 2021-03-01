namespace PaymentGateway
{
    using System;
    using System.Collections.Concurrent;
    using global::PaymentGateway.Models;

    public class PaymentStorage : IPaymentStorage
    {
        private readonly ConcurrentDictionary<string, PaymentDetails> storage;

        public PaymentStorage()
        {
            this.storage = new ConcurrentDictionary<string, PaymentDetails>();
        }

        public bool SavePaymentDetails(PaymentRequest request, PaymentResponse response)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var details = new PaymentDetails
            {
                Identifier = response.Identifier,
                Error = response.Error,
                MaskedCardNumber = $"************{request.CardNumber[12..]}",
            };

            return this.storage.TryAdd(details.Identifier, details);
        }

        public PaymentDetails RetrievePaymentDetails(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            if (this.storage.TryGetValue(identifier, out var paymentDetails))
            {
                return paymentDetails;
            }

            return null;
        }
    }
}
