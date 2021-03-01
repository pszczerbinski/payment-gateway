namespace PaymentGateway
{
    using System;
    using System.Collections.Concurrent;
    using global::PaymentGateway.Models;
    using Microsoft.Extensions.Logging;

    public class PaymentStorage : IPaymentStorage
    {
        private readonly ConcurrentDictionary<string, PaymentDetails> storage;
        private readonly ILogger<PaymentStorage> logger;

        public PaymentStorage(ILoggerFactory loggerFactory)
        {
            if (loggerFactory != null)
            {
                this.logger = loggerFactory.CreateLogger<PaymentStorage>();
            }

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

            this.logger?.LogInformation(
                "Saving transaction with id: {identifier}.",
                details.Identifier);

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
                this.logger?.LogInformation(
                    "Successfully retrieved transaction with id: {identifier}.",
                    identifier);

                return paymentDetails;
            }

            this.logger?.LogInformation(
                "Transaction with id {identifier} was not found.",
                identifier);

            return null;
        }
    }
}
