using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Retail.Product.API.Interfaces.Repositories.Mappers;
using Retail.Product.API.Repositories.Mappers;

namespace Retail.Product.API.Tests.UnitTests.Repositories.Mappers
{
    [TestClass]
    public class RedskyProductMapperShould
    {
        private IRedskyProductMapper _redskyProductMapper;
        private IFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _redskyProductMapper = new RedskyProductMapper();
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Map_Should_Return_Product()
        {
            // Setup
            var redSkyProduct = new Model.RedskyProduct
            {
                item = new Model.RedskyItem
                {
                    tcin = _fixture.Create<int>().ToString(),
                    product_description = new Model.RedskyItemDescription
                    {
                        title = _fixture.Create<string>()
                    }
                }
            };

            // Act
            var result = _redskyProductMapper.MapToInternalProductModel(redSkyProduct);

            // Assert
            result.ProductId.Should().Equals(redSkyProduct.item.tcin);
        }
    }
}
