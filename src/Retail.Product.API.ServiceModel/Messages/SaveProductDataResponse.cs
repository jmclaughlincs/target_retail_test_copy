using ServiceStack;

namespace Retail.Product.API.ServiceModel.Messages
{
    /// <summary>
    /// Response object for price save
    /// </summary>
    public class SaveProductDataResponse : IHasResponseStatus
    {
        /// <summary>
        /// boolean for easy check on status of save
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// servicestack response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }
    }
}
