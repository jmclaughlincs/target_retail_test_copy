using Retail.Product.API.Interfaces.Managers;
using Messages = Retail.Product.API.ServiceModel.Messages;
using ServiceStack;
using System;
using System.Threading.Tasks;

namespace Retail.Product.API.ServiceDefinition
{
	public class RetailProductService : Service
	{
		private readonly IRetailProductManager _retailProductManager;

		public RetailProductService(IRetailProductManager retailProductManager)
		{
            _retailProductManager = retailProductManager;
		}

		public async Task<Messages.ReadProductDataResponse> Get(Messages.ReadProductData request)
		{
			try
			{
				var response = new Messages.ReadProductDataResponse();
				response.ProductData = _retailProductManager.GetProductData(request.ProductId);

				return response;
			}
			catch(Exception ex)
			{
				//Log exceptions here - check logging assumptions in readme
				throw;
			}
		}

		public Messages.SaveProductDataResponse Put(Messages.SaveProductData request)
		{
			try
			{
				var response = new Messages.SaveProductDataResponse();
				response.Status = _retailProductManager.SetProductPrice(request.ProductId, request.Price);

				return response;
			}
			catch (Exception ex)
			{
                //Log exceptions here - check logging assumptions in readme
                throw;
			}
		}
	}
}
