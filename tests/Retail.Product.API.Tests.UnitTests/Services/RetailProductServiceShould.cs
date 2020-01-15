using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Retail.Product.API.Interfaces.Managers;
using Retail.Product.API.ServiceDefinition;
using Retail.Product.API.ServiceModel.Messages;
using Retail.Product.API.ServiceModel.Types;
using System.Net;
using System.Threading.Tasks;

namespace Retail.Product.API.Tests.UnitTests.Services
{
    [TestClass]
    public class RetailProductServiceShould
    {
        private Mock<IRetailProductManager> _mockRetailProductManager;

        private Fixture _fixture;
        private RetailProductService _retailProductService;
        private ProductData _product;

        [TestInitialize]
        public void Initialize()
        {
            _mockRetailProductManager = new Mock<IRetailProductManager>();
            _fixture = new Fixture();
            _retailProductService = new RetailProductService(_mockRetailProductManager.Object);
            _product = _fixture.Create<ProductData>();
        }

        [TestMethod]
        public async Task ReadProductData()
        {
            int testId = _product.ProductId;
            // Setup
            _mockRetailProductManager
                .Setup(rpm => rpm.GetProductData(testId))
                .Returns(_product);

            // Act
            var request = new ReadProductData();
            request.ProductId = testId;
            var response = await _retailProductService.Get(request);

            // Assert
            response.Should().NotBeNull();
            response.ProductData.ProductId.Should().Equals(request.ProductId);
            response.ProductData.Title.Should().NotBeNull();
            response.ProductData.Price.Should().NotBeNull();
        }

        [TestMethod]
        public void ReadProductData_Throws_Exception()
        {
            // Setup
            var exceptionMessage = "Expected Exception";
            var request = new ReadProductData();

            _mockRetailProductManager
                .Setup(rpm => rpm.GetProductData(It.IsAny<int>()))
                .Throws(new WebException(exceptionMessage));

            //Act
            _retailProductService.Awaiting(x => x.Get(request))
               .Should().Throw<WebException>().WithMessage(exceptionMessage);

            //Assert
            _mockRetailProductManager
                    .Verify(rpm => rpm.GetProductData(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void SaveProductData()
        {
            int testId = _product.ProductId;
            string testPrice = "1";
            // Setup
            _mockRetailProductManager
                .Setup(rpm => rpm.SetProductPrice(testId, testPrice))
                .Returns(true);

            // Act
            var request = new SaveProductData();
            request.ProductId = testId;
            request.Price = testPrice;
            var response =  _retailProductService.Put(request);

            // Assert
            response.Should().NotBeNull();
            response.Status.Should().BeTrue();
        }

        [TestMethod]
        public void SaveProductData_Throws_Exception()
        {
            // Setup
            var exceptionMessage = "Expected Exception";
            var request = new SaveProductData();

            _mockRetailProductManager
                .Setup(rpm => rpm.SetProductPrice(It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new WebException(exceptionMessage));

            //Act
            _retailProductService.Invoking(x => x.Put(request))
               .Should().Throw<WebException>().WithMessage(exceptionMessage);

            //Assert
            _mockRetailProductManager
                    .Verify(rpm => rpm.SetProductPrice(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}
