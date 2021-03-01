namespace PaymentGateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents a currency.
    /// </summary>
    public class Currency
    {
        /// <summary> Pound Sterling Currency </summary>
        public static readonly Currency GBP = new Currency("Pound Sterling", "GBP");

        /// <summary> Euro Currency </summary>
        public static readonly Currency EURO = new Currency("Euro", "EUR");

        private static readonly Lazy<List<Currency>> Storage =
            new Lazy<List<Currency>>(ListAllOptions);

        private Currency(string name, string code)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            this.Name = name;
            this.Code = code;
        }

        /// <summary>
        /// Gets the name of the currency.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the currency code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets all if the supported currencies.
        /// </summary>
        public static IReadOnlyCollection<Currency> GetAllOptions => Storage.Value;
        
        /// <summary>
        /// Create an instance from currency code.
        /// </summary>
        /// <param name="code">The currency code.</param>
        /// <returns><see cref="Currency"/> instance.</returns>
        public static Currency FromCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var result = Storage.Value.FirstOrDefault(s => s.Code.Equals(code));
            if (result == null)
            {
                throw new InvalidCastException(
                    $"Provided currency code is invalid: {code}.");
            }

            return result;
        }

        private static List<Currency> ListAllOptions()
        {
            var t = typeof(Currency);
            return t.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(p => t.IsAssignableFrom(p.FieldType))
                    .Select(pi => (Currency)pi.GetValue(null))
                    .OrderBy(p => p.Name)
                    .ToList();
        }
    }
}
