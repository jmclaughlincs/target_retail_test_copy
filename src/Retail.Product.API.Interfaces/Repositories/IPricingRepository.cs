using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Product.API.Interfaces.Repositories
{
    public interface IPricingRepository
    {
        string GetProductPrice(int productId);

        bool SetProductPrice(int productId, string price);
    }
}
