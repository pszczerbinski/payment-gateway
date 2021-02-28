namespace PaymentGateway.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IntegrationTestSettings
    {
        public static string Address { get; private set; }

        [AssemblyInitialize]
        public static void Initialise(TestContext context)
        {
            Address = context.Properties["Address"].ToString();
        }
    }
}
