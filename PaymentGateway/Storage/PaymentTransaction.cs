namespace PaymentGateway.Storage
{
    using System.ComponentModel.DataAnnotations;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Defines a payment transaction to be stored in MongoDB.
    /// </summary>
    public class PaymentTransaction
    {
        /// <summary>
        /// Gets the MongoDB object identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Identifier { get; set; }

        /// <summary>
        /// Gets the payment identifier.
        /// </summary>
        [Required]
        public string PaymentIdentifier { get; set; }

        /// <summary>
        /// Gets the value indicating whether the transaction was successful.
        /// </summary>
        [Required]
        public bool Success { get; set; }

        /// <summary>
        /// Gets a transaction error, if occurred.
        /// </summary>
        [Required]
        public string Error { get; set; }

        /// <summary>
        /// Gets the masked card number used in the transaction.
        /// </summary>
        [Required]
        public string MaskedCardNumber { get; set; }

        /// <summary>
        /// Gets the transaction amount.
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// Gets the transaction currency.
        /// </summary>
        [Required]
        public string Currency { get; set; }
    }
}
