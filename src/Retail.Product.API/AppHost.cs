using Funq;
using Retail.Product.API.ServiceDefinition;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Text;
using ServiceStack.Validation;

namespace Retail.Product.API
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost() : base(ServiceDefinitionInfo.Name, ServiceDefinitionInfo.Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            JsConfig.EmitCamelCaseNames = true;
            JsConfig.DateHandler = DateHandler.ISO8601;

            ContainerManager.Register(container);

            InitializePlugins(container);
            InitializeContainer(container);
        }


        private void InitializePlugins(Container container)
        {
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new SwaggerFeature());
            var corsFeature = new CorsFeature(allowedHeaders: "X-Request-With, Content-Type, Authorization, Origin, Accept");
            Plugins.Add(corsFeature);
            Plugins.RemoveAll(x => x is NativeTypesFeature);
        }

        private void InitializeContainer(Container container)
        {
        }
    }
}
