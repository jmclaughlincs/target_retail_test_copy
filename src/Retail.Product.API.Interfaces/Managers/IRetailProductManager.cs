using Retail.Product.API.ServiceModel.Types;
using System.Threading.Tasks;

namespace Retail.Product.API.Interfaces.Managers
{
	public interface IRetailProductManager
    {
        ProductData GetProductData(int productId);
        bool SetProductPrice(int productId, string price);
    }
}
