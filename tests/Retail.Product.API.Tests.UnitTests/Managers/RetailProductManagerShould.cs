using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Retail.Product.API.Interfaces.Managers;
using Retail.Product.API.Interfaces.Repositories;
using Retail.Product.API.Managers;
using Retail.Product.API.ServiceModel.Types;
using System;

namespace Retail.Product.API.Tests.UnitTests.Managers
{
    [TestClass]
	public class RetailProductManagerShould
	{
		private Fixture _fixture;
		private Mock<IRetailProductRepository> _mockRetailProductRepo;
        private Mock<IPricingRepository> _mockPricingRepo;
		private IRetailProductManager _retailProductManager;

		[TestInitialize]
		public void TestInit()
		{
			_fixture = new Fixture();
            _mockRetailProductRepo = new Mock<IRetailProductRepository>();
            _mockPricingRepo = new Mock<IPricingRepository>();
            _retailProductManager = new RetailProductManager(_mockRetailProductRepo.Object, _mockPricingRepo.Object);
		}

		[TestMethod]
		public void GetProductData_Should_Get_Data()
		{
            // Setup
            int productId = _fixture.Create<int>();
            ProductData returnData = _fixture.Build<ProductData>().With(r => r.ProductId, productId).Create();

            _mockRetailProductRepo
                .Setup(repo => repo.GetProduct(productId))
                .Returns(returnData);

            _mockPricingRepo
                .Setup(repo => repo.GetProductPrice(productId))
                .Returns(returnData.Price);

            // Act
            var result = _retailProductManager.GetProductData(productId);

            // Assert
            _mockRetailProductRepo.Verify(repo => repo.GetProduct(productId), Times.Once);
            _mockPricingRepo.Verify(repo => repo.GetProductPrice(productId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetProductData_Handles_Exception()
        {
            // Setup
            int productId = _fixture.Create<int>();

            _mockRetailProductRepo
                .Setup(repo => repo.GetProduct(productId))
                .Returns((ProductData)null);

            // Act
            var result = _retailProductManager.GetProductData(productId);

            // Assert
            Assert.Fail("Should have thrown Exception and not gotten this far");
        }

        [TestMethod]
        public void SetProductPrice_Should_Set_Price()
        {
            // Setup
            int productId = _fixture.Create<int>();
            string price = "1";

            _mockPricingRepo
               .Setup(repo => repo.SetProductPrice(productId, price))
               .Returns(true);

            // Act
            var result = _retailProductManager.SetProductPrice(productId, price);

            // Assert
            _mockPricingRepo.Verify(repo => repo.SetProductPrice(productId, price), Times.Once);
        }
    }
}
