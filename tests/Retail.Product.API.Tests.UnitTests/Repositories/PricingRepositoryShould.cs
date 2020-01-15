using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Retail.Product.API.Interfaces.Repositories;
using Retail.Product.API.Repositories;
using StackExchange.Redis;
using System;

namespace Retail.Product.API.Tests.UnitTests.Repositories
{
    [TestClass]
    public class PricingRepositoryShould
    {
        private IPricingRepository _pricingRepository;
        private Mock<IConnectionMultiplexer> _connMultiplexer;
        private IFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _connMultiplexer = new Mock<IConnectionMultiplexer>();
            _fixture = new Fixture();
            _pricingRepository = new PricingRepository(_connMultiplexer.Object);
        }

        [TestMethod]
        public void GetProductPrice_Should_Return_Price()
        {
            // Setup
            int productId = _fixture.Create<int>();
            var expectedResult = _fixture.Create<string>();
            var mockDatabase = new Mock<IDatabase>();
            RedisValue convertedToRedisValue = expectedResult;

            _connMultiplexer.Setup(x => x.IsConnected).Returns(false);

            mockDatabase
                .Setup(x => x.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(convertedToRedisValue);

            _connMultiplexer
                 .Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                 .Returns(mockDatabase.Object);

            // Act
            var result = _pricingRepository.GetProductPrice(productId);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void GetProductPrice_Should_Handle_Bad_Data()
        {
            // Setup
            int productId = _fixture.Create<int>();
            var mockDatabase = new Mock<IDatabase>();

            _connMultiplexer.Setup(x => x.IsConnected).Returns(false);

            mockDatabase
                .Setup(x => x.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .Returns(RedisValue.Null);

            _connMultiplexer
                 .Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                 .Returns(mockDatabase.Object);

            // Act
            var result = _pricingRepository.GetProductPrice(productId);

            // Assert
            mockDatabase
                    .Verify(rpm => rpm.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()), Times.Once);
        }

        [TestMethod]
        public void SetProductPrice_Should_Return_bool()
        {
            // Setup
            int productId = _fixture.Create<int>();
            string price = _fixture.Create<int>().ToString();
            var mockDatabase = new Mock<IDatabase>();

            _connMultiplexer
                .Setup(x => x.IsConnected)
                .Returns(false);

            mockDatabase
                .Setup(x => x.StringSet(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan>(), It.IsAny<When>(), It.IsAny<CommandFlags>()))
                .Returns(true);

            _connMultiplexer
                 .Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                 .Returns(mockDatabase.Object);

            // Act
            var result = _pricingRepository.SetProductPrice(productId, price);

            // Assert
            mockDatabase.Verify(x => x.StringSet(productId.ToString(), price, null, When.Always, CommandFlags.None));
        }

        [TestMethod]
        public void SetProductPrice_Should_Handle_Empty_Input()
        {
            // Setup
            int productId = _fixture.Create<int>();
            string price = string.Empty;
            var mockDatabase = new Mock<IDatabase>();

            _connMultiplexer
                .Setup(x => x.IsConnected)
                .Returns(false);

            mockDatabase
                .Setup(x => x.StringSet(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan>(), It.IsAny<When>(), It.IsAny<CommandFlags>()))
                .Returns(true);

            _connMultiplexer
                 .Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                 .Returns(mockDatabase.Object);

            // Act
            var result = _pricingRepository.SetProductPrice(productId, price);

            // Assert
            mockDatabase.Verify(x => x.StringSet(productId.ToString(), price, null, When.Always, CommandFlags.None));
        }
    }
}
