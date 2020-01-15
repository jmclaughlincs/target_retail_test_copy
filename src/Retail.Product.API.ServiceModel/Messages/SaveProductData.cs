using ServiceStack;

namespace Retail.Product.API.ServiceModel.Messages
{
    /// <summary>
    /// Save route for product pricing
    /// </summary>
    [Route("/RetailProduct/{ProductId}", "PUT", Summary = "Save product pricing data", Notes = "Saves the given price for the given product id")]
    public class SaveProductData : IReturn<SaveProductDataResponse>
    {
        /// <summary>
        /// ID of product
        /// </summary>
        [ApiMember(Description = "Product ID", IsRequired = true)]
        public int ProductId { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        [ApiMember(Description = "Price", IsRequired = true)]
        public string Price { get; set; }
    }
}
