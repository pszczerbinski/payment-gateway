namespace PaymentGateway.Storage
{
    using System;
    using System.Threading.Tasks;
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Converters;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    public class MongoPaymentStorage : IPaymentStorage
    {
        private readonly ILogger<MongoPaymentStorage> logger;
        private readonly IMongoPaymentStorageContext context;

        public MongoPaymentStorage(ILoggerFactory loggerFactory, IMongoPaymentStorageContext context)
        {
            if (loggerFactory != null)
            {
                this.logger = loggerFactory.CreateLogger<MongoPaymentStorage>();
            }

            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> SavePaymentDetailsAsync(PaymentRequest request, PaymentResponse response)
        {
            var transaction = MongoModelConverter.To(request, response);
            if (transaction == null)
            {
                return false;
            }

            this.logger?.LogInformation(
                "Trying to save payment with id: {identifier}.",
                response.Identifier);

            await this.context.Transactions.InsertOneAsync(transaction);

            return true;
        }

        public async Task<PaymentDetails> RetrievePaymentDetailsAsync(string identifier)
        {
            this.logger?.LogInformation(
                "Trying to retrieve payment with id: {identifier}.",
                identifier);

            var transaction = await this.context.Transactions
                .AsQueryable()
                .Where(o => o.PaymentIdentifier == identifier)
                .FirstOrDefaultAsync();

            return MongoModelConverter.From(transaction);
        }
    }
}
