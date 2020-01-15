using Retail.Product.API.Interfaces.Repositories.Mappers;
using Retail.Product.API.ServiceModel.Types;
using System;

namespace Retail.Product.API.Repositories.Mappers
{
	public class RedskyProductMapper : IRedskyProductMapper
	{
		public ServiceModel.Types.ProductData MapToInternalProductModel(Model.RedskyProduct product)
		{
            ProductData productData = new ProductData();
            try
            {
                productData.ProductId = int.Parse(product.item.tcin);
                productData.Title = product.item.product_description.title;
            }
            catch(Exception ex)
            {
                //log exception here.  for the purposes of this mapper, we're ok swallowing the exception and handling an
                //empty ProductData object elsewhere.
                //We could be swallowing:
                    // ArgumentNullException, in case the product, product.item, or product.item.tcin is null
                    // FormatException if we're getting something that isn't of the expected input
                    // OverflowException if we'd get higher than int32
            }

            return productData;
		}
	}
}
