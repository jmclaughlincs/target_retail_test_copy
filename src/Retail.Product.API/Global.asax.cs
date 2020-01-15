using System;
using System.Diagnostics.CodeAnalysis;

namespace Retail.Product.API
{
    [ExcludeFromCodeCoverage]
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {            
            // Set ServiceStack's LogFactory
            ServiceStack.Logging.LogManager.LogFactory = new ServiceStack.Logging.Log4Net.Log4NetFactory();
                        
            log4net.GlobalContext.Properties["AppName"] = "Retail.Product.API";
            
            log4net.Config.XmlConfigurator.Configure();

            new AppHost().Init();
        }
    }
}
