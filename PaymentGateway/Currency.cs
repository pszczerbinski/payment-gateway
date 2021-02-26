namespace PaymentGateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Currency
    {
        public static readonly Currency GBP = new Currency("Pound Sterling", "GBP");
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

        public string Name { get; }

        public string Code { get; }

        public static IReadOnlyCollection<Currency> GetAllOptions => Storage.Value;
        
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
