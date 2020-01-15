namespace Retail.Product.API.ServiceModel.Messages
{
    /// <summary>
    /// Response object from redsky data api
    /// </summary>
    public class ReadRedskyProductInfoResponse
    {
        /// <summary>
        /// product model
        /// </summary>
        public Model.RedskyProduct product { get; set; }
    }
}
