using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retail.Product.API.ServiceModel.Messages;
using ServiceStack;
using System.Configuration;

namespace Retail.Product.API.Tests.AcceptanceTests
{
    [TestClass]
	public class RetailProductServiceShould
	{
        protected string _serviceUrl;

        /*
         * JMM - I would generally handle pulling in different key variables for different environments here
         *   in order to facilitate CD/CI build processes.
         *
         *   Also of note, some of these methods could of course be await/async methods, but with this small of a case
         *   that actually adds very unnecessary overhead.
         */

        [TestInitialize]
		public void TestInit()
		{
            _serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        }

        [TestMethod]
        public void AAT_ReadProductData()
        {
            using (IServiceClient client = GetJsonServiceClient(_serviceUrl))
            {
                ReadProductData request = new ReadProductData {
                    ProductId = 13860428
                };

                var response = client.Get(request);

                response.Should().NotBeNull();
                response.ProductData.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void AAT_SaveProductData()
        {
            using (IServiceClient client = GetJsonServiceClient(_serviceUrl))
            {
                SaveProductData request = new SaveProductData
                {
                    ProductId = 13860428,
                    Price = "2"
                };

                var response = client.Put(request);

                response.Should().NotBeNull();
                response.Status.Should().Be(true);
            }
        }

        public JsonServiceClient GetJsonServiceClient(string url)
        {
            var client = new JsonServiceClient(url);
            client.Headers.Add("UserName", "RetailProductAPI_AAT");
            return client;
        }
    }
}
