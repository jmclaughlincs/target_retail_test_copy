using Retail.Product.API.Interfaces.Managers;
using Retail.Product.API.Interfaces.Repositories;
using Retail.Product.API.Model;
using Retail.Product.API.ServiceModel.Types;
using System;
using System.Threading.Tasks;
using System.Linq;
using ServiceStack;
using System.Collections.Generic;
using InternalModelDto = Retail.Product.API.Model;

namespace Retail.Product.API.Managers
{
	public class RetailProductManager : IRetailProductManager
    {
		private readonly IRetailProductRepository _retailProductRepository;
        private readonly IPricingRepository _pricingRepository;

		public RetailProductManager(IRetailProductRepository retailProductRepository, IPricingRepository pricingRepository)
		{
            _retailProductRepository = retailProductRepository;
            _pricingRepository = pricingRepository;
		}

		public ProductData GetProductData(int productId)
		{
			var productData = _retailProductRepository.GetProduct(productId);
            if (productData == null)
            {
                //Here, I would definitely NOT throw the base Exception class if this wasn't a case study.
                //Adding extra boilerplate just to wrap Exception in another class seemed unnecessary at the moment, though.
                throw new Exception("Unable to read product information");
            }

            productData.Price = _pricingRepository.GetProductPrice(productId) ?? "0.00";

            return productData;
		}

        public bool SetProductPrice(int productId, string price)
        {
            return _pricingRepository.SetProductPrice(productId, price);
        }
    }
}
