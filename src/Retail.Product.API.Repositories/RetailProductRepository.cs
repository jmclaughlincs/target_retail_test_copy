using Retail.Product.API.Interfaces.Repositories;
using Retail.Product.API.Interfaces.Repositories.Mappers;
using System.Threading.Tasks;
using ServiceStack;
using Messages = Retail.Product.API.ServiceModel.Messages;
using System.Configuration;
using System.Net;

namespace Retail.Product.API.Repositories
{
	public class RetailProductRepository : IRetailProductRepository
	{
        private readonly IJsonServiceClient _jsonServiceClient;
        private readonly IRedskyProductMapper _redskyProductMapper;

        public RetailProductRepository(IJsonServiceClient jsonServiceClient, IRedskyProductMapper redskyProductMapper)
        {
           _jsonServiceClient = jsonServiceClient;
          
            _redskyProductMapper = redskyProductMapper;
        }

        public ServiceModel.Types.ProductData GetProduct(int productId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string parms = "excludes=taxonomy,price,promotion,bulk_ship,rating_and_review_reviews,rating_and_review_statistics,question_answer_statistics,available_to_promise_network,deep_red_labels";
            string clientUri = string.Format("{0}?{1}", productId, parms);

            var response = _jsonServiceClient.Get<Messages.ReadRedskyProductInfoResponse>(clientUri);

            return _redskyProductMapper.MapToInternalProductModel(response.product);
        }
    }
}
