namespace PaymentGateway.Storage
{
    using Microsoft.Extensions.Logging;

    using MongoDB.Driver;

    /// <summary>
    /// Implementation of <see cref="IMongoPaymentStorageContext"/>.
    /// </summary>
    public class MongoPaymentStorageContext : IMongoPaymentStorageContext
    {
        private readonly ILogger<MongoPaymentStorageContext> logger;
        private readonly IMongoDatabase db;

        public MongoPaymentStorageContext(
            ILoggerFactory loggerfactory,
            IDatabaseSettings settings)
        {
            this.logger = loggerfactory.CreateLogger<MongoPaymentStorageContext>();

            var user = settings.Username;
            var password = settings.Password;
            var hosts = settings.Hosts;
            var database = settings.Name;
            var connectionString = $@"mongodb://{user}:{password}@{hosts}";
            var connectionStringNoPass = $@"mongodb://{user}:PASSWORD@{hosts}";

            this.logger.LogInformation("Creating mongo client: {connString} (Password omitted)", connectionStringNoPass);
            var client = new MongoClient(connectionString);

            this.logger.LogInformation("Connecting to mongo database: {database}", database);
            this.db = client.GetDatabase(database);

            this.Transactions = this.db.GetCollection<PaymentTransaction>("PaymentTransactions");

            this.logger.LogInformation("Successfully connected to database: {database}", database);
        }

        public IMongoCollection<PaymentTransaction> Transactions { get; }
    }
}
