﻿namespace PaymentGateway
{
    using System;
    using System.Threading.Tasks;
    using Bank;
    using global::PaymentGateway.Converters;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Storage;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Implementation of the payment gateway.
    /// </summary>
    public class PaymentGateway : IPaymentGateway
    {
        private readonly IBank bank;
        private readonly IPaymentStorage storage;
        private readonly ILogger<PaymentGateway> logger;

        public PaymentGateway(ILoggerFactory loggerFactory, IBank bank, IPaymentStorage storage)
        {
            if (loggerFactory != null)
            {
                this.logger = loggerFactory.CreateLogger<PaymentGateway>();
            }

            this.bank = bank ?? throw new ArgumentNullException(nameof(bank));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public async Task<PaymentResponse> ProcessPaymentRequestAsync(PaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.logger?.LogInformation(
                "Processing new payment for {amount} {currency}.",
                request.Amount,
                request.Currency.Code);

            var bankRequest = BankModelConverter.From(request);
            var banksResponse = await this.bank.ProcessPaymentAsync(bankRequest);
            var response = BankModelConverter.To(banksResponse);
            var saveResult = await this.storage.SavePaymentDetailsAsync(request, response);
            if (!saveResult)
            {
                // Could retry the save operation.
                this.logger?.LogError(
                    "Failed to save transaction with id: {identifier}.",
                    response.Identifier);
            }

            return response;
        }

        public async Task<PaymentDetails> RetrievePaymentDetailsAsync(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            this.logger?.LogInformation(
                "Retrieving payment details for id: {identifier}.",
                identifier);

            var details = await this.storage.RetrievePaymentDetailsAsync(identifier);
            return details;
        }
    }
}
