using System.Collections.Generic;
using Retail.Product.API.Model;
using Retail.Product.API.ServiceModel.Types;

namespace Retail.Product.API.Interfaces.Repositories.Mappers
{
    public interface IRedskyProductMapper
    {
        ServiceModel.Types.ProductData MapToInternalProductModel(Model.RedskyProduct product);
	}
}
