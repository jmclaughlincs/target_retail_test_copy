using Funq;
using Retail.Product.API.Interfaces.Managers;
using Retail.Product.API.Interfaces.Repositories;
using Retail.Product.API.Interfaces.Repositories.Mappers;
using Retail.Product.API.Managers;
using Retail.Product.API.Repositories;
using Retail.Product.API.Repositories.Mappers;
using ServiceStack;
using ServiceStack.Validation;
using StackExchange.Redis;
using System;
using System.Configuration;

namespace Retail.Product.API
{
    public static class ContainerManager
    {
        public static void Register(Container container)
        {
            // add mappers
            container.RegisterAutoWiredAs<RedskyProductMapper, IRedskyProductMapper>();

			// add repos
            IJsonServiceClient _redskyServiceClient = new JsonServiceClient()
            {
                BaseUri = ConfigurationManager.AppSettings["RedskyApiUrl"],
                RequestFilter = req =>
                {
                    req.Headers.Add("X-UserId", Environment.UserName);
                    req.Headers.Add("X-SourceSystem", "Retail.Product.Api");
                }
            };
            container.Register<IRetailProductRepository>(new RetailProductRepository(_redskyServiceClient, container.Resolve<IRedskyProductMapper>()));

            IConnectionMultiplexer _muxer = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisUrl"]);
            container.Register<IPricingRepository>(new PricingRepository(_muxer));


            // add managers
            container.RegisterAutoWiredAs<RetailProductManager, IRetailProductManager>();
        }
    }
}
