namespace PaymentGateway.Storage
{
    using MongoDB.Driver;

    /// <summary>
    /// Defines a MongoDB payment storage context.
    /// </summary>
    public interface IMongoPaymentStorageContext
    {
        /// <summary>
        /// Gets the transactions in the MongoDB.
        /// </summary>
        public IMongoCollection<PaymentTransaction> Transactions { get; }
    }
}
