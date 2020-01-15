using Retail.Product.API.Model;
using Retail.Product.API.ServiceModel.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retail.Product.API.Interfaces.Repositories
{
	public interface IRetailProductRepository
	{
        ProductData GetProduct(int productId);
    }
}
