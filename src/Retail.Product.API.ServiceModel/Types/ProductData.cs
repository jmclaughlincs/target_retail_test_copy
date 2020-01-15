namespace Retail.Product.API.ServiceModel.Types
{
    /// <summary>
    /// Product object
    /// </summary>
    public class ProductData
    {
        /// <summary>
        /// ID of product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Title / Name of product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        public string Price { get; set; }
    }
}
