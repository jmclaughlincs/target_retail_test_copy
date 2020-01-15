using Retail.Product.API.ServiceModel.Types;
using ServiceStack;

namespace Retail.Product.API.ServiceModel.Messages
{
    /// <summary>
    /// Response for Read Product
    /// </summary>
    public class ReadProductDataResponse : IHasResponseStatus
    {
        /// <summary>
        /// Product data object
        /// </summary>
        public ProductData ProductData { get; set; }

        /// <summary>
        /// Response Status from ServiceStack
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }
    }
}
