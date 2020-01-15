using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Retail.Product.API.Interfaces.Repositories;
using Retail.Product.API.Repositories;
using Retail.Product.API.Repositories.Mappers;
using ServiceStack;
using Messages = Retail.Product.API.ServiceModel.Messages;
using ServiceModalDto = Retail.Product.API.ServiceModel.Types;

namespace Retail.Product.API.Tests.UnitTests.Repositories
{
    [TestClass]
    public class RetailProductRepositoryShould
    {
        private IRetailProductRepository _retailProductRepository;
        private Mock<IJsonServiceClient> _mockJsonServiceClient;
        private IFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _mockJsonServiceClient = new Mock<IJsonServiceClient>();
            _fixture = new Fixture();
            _retailProductRepository = new RetailProductRepository(_mockJsonServiceClient.Object, new RedskyProductMapper());
        }

        [TestMethod]
        public void GetProduct_Should_Return_Product()
        {
            // Setup
            var serviceModalDto = _fixture.Create<ServiceModalDto.ProductData>();
            var mockResponse = new Messages.ReadRedskyProductInfoResponse
            {
                product = new Model.RedskyProduct {
                    item = new Model.RedskyItem {
                        tcin = serviceModalDto.ProductId.ToString(),
                        product_description = new Model.RedskyItemDescription {
                            title = _fixture.Create<string>()
                        }
                    }
                }    
            };

            _mockJsonServiceClient
                .Setup(x => x.Get<Messages.ReadRedskyProductInfoResponse>(It.IsAny<string>()))
                .Returns(mockResponse);

            // Act
            var result = _retailProductRepository.GetProduct(serviceModalDto.ProductId);

            // Assert
            result.ProductId.Should().Be(serviceModalDto.ProductId);
        }

        [TestMethod]
        public void GetProduct_ShouldReturn_Empty__IfGiven_EmptyOrBadData()
        {
            // Setup
            var serviceModalDto = _fixture.Create<ServiceModalDto.ProductData>();
            var mockResponse = new Messages.ReadRedskyProductInfoResponse();

            _mockJsonServiceClient
                .Setup(x => x.Get<Messages.ReadRedskyProductInfoResponse>(It.IsAny<string>()))
                .Returns(mockResponse);

            //Act
            var result = _retailProductRepository.GetProduct(serviceModalDto.ProductId);

            //Assert
            result.ProductId.Should().Be(0);
            result.Title.Should().Be(null);
            result.Price.Should().Be(null);
        }
    }
}
