namespace PaymentGateway.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class TestsCurrencyClass
    {
        [TestMethod]
        public void ThatCanCreateGbpCurrency()
        {
            var gbp = Currency.GBP;

            gbp.Name.ShouldBe("Pound Sterling");
            gbp.Code.ShouldBe("GBP");
        }

        [TestMethod]
        public void ThatCanCreateEurCurrency()
        {
            var euro = Currency.EURO;

            euro.Name.ShouldBe("Euro");
            euro.Code.ShouldBe("EUR");
        }

        [DataTestMethod]
        [DataRow("GBP", "Pound Sterling")]
        [DataRow("EUR", "Euro")]
        public void ThatCanCreateCurrencyFromCode(string code, string name)
        {
            var currency = Currency.FromCode(code);

            currency.Name.ShouldBe(name);
            currency.Code.ShouldBe(code);
        }

        [TestMethod]
        public void ThatCannotCreateCurrencyFromInvalidCode()
        {
            var code = "USD";
            
            Should.Throw<InvalidCastException>(() => Currency.FromCode(code));
        }
    }
}
