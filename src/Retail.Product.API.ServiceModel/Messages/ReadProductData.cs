using ServiceStack;

namespace Retail.Product.API.ServiceModel.Messages
{
    /// <summary>
    /// Message for Read route
    /// </summary>
    [Route("/RetailProduct/{ProductId}", "GET", Summary = "Read product data", Notes = "Reads product data from multiple sources to return a new object")]
    public class ReadProductData : IReturn<ReadProductDataResponse>
    {
        /// <summary>
        /// unique identifier for product
        /// </summary>
        [ApiMember(Description = "Product ID", IsRequired = true)]
        public int ProductId { get; set; }
    }
}
