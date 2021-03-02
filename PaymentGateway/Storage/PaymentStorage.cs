namespace PaymentGateway.Storage
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using global::PaymentGateway.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Simple implementation of <see cref="IPaymentStorage"/> storage layer.
    /// </summary>
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

        public Task<bool> SavePaymentDetailsAsync(PaymentRequest request, PaymentResponse response)
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
                Success = response.Success,
                Error = response.Error,
                MaskedCardNumber = $"************{request.CardNumber[12..]}",
                Amount = request.Amount,
                Currency = request.Currency.Code,
            };

            this.logger?.LogInformation(
                "Saving transaction with id: {identifier}.",
                details.Identifier);

            return Task.FromResult(this.storage.TryAdd(details.Identifier, details));
        }

        public Task<PaymentDetails> RetrievePaymentDetailsAsync(string identifier)
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

                return Task.FromResult(paymentDetails);
            }

            this.logger?.LogInformation(
                "Transaction with id {identifier} was not found.",
                identifier);

            return null;
        }
    }
}
