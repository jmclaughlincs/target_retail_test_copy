namespace Retail.Product.API.Model
{
    /// <summary>
    /// Model for Item json
    /// </summary>
    public class RedskyItem
    {
        /// <summary>
        /// internal product id
        /// </summary>
        public string tcin { get; set; }
        /// <summary>
        /// internal description
        /// </summary>
        public RedskyItemDescription product_description { get; set; }
    }
}
