using Retail.Product.API.Interfaces.Repositories;
using StackExchange.Redis;

namespace Retail.Product.API.Repositories
{
    public class PricingRepository : IPricingRepository
    {
        private readonly IConnectionMultiplexer _muxer;

        public PricingRepository(IConnectionMultiplexer muxer)
        {
            _muxer = muxer;
        }

        /// <summary>
        /// Get Product Price from nosql db
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public string GetProductPrice(int productId)
        {
            IDatabase conn = _muxer.GetDatabase();
            return conn.StringGet(productId.ToString());
        }

        /// <summary>
        /// Set Product Price in nosql db
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool SetProductPrice(int productId, string price)
        {
            IDatabase conn = _muxer.GetDatabase();
            return conn.StringSet(productId.ToString(), price, null, When.Always, CommandFlags.None);
        }
    }
}
