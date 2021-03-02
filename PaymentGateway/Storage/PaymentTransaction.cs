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
        /// Gets or sets the MongoDB object identifier.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Identifier { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        [Required]
        public string PaymentIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the transaction was successful.
        /// </summary>
        [Required]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a transaction error, if occurred.
        /// </summary>
        [Required]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the masked card number used in the transaction.
        /// </summary>
        [Required]
        public string MaskedCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the transaction currency.
        /// </summary>
        [Required]
        public string Currency { get; set; }
    }
}
