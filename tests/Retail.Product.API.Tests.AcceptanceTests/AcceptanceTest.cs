using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System;
using System.Configuration;
using System.Linq;

namespace Retail.Product.API.Tests.AcceptanceTests
{
    [TestClass]
    public class AcceptanceTest
    {
        private IJsonServiceClient _jsonClient;

        [TestInitialize]
        public void InitializeTestData()
        {
            Console.WriteLine("--AppSettings--");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                Console.WriteLine($"{key}: '{ConfigurationManager.AppSettings[key]}'");
            }
            Console.WriteLine("--End AppSettings--");
            _jsonClient = new JsonServiceClient(ConfigurationManager.AppSettings["ServiceUrl"]);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _jsonClient.Dispose();
        }
    }
}

