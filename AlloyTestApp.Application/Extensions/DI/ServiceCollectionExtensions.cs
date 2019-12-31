using AlloyTestApp.Application.AppConsts;
using AlloyTestApp.Application.BusinessLogic;
using AlloyTestApp.Application.DataAccess.DataProviders;
using AlloyTestApp.Application.DataAccess.RepositoryFactory;
using AlloyTestApp.Application.Exceptions;
using AlloyTestApp.Core.Interfaces.BusinessLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlloyTestApp.Application.Extensions.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryFactory(this IServiceCollection services, Action<RepositoryFactoryOptions> configuration)
        {
            RepositoryFactoryOptions options = new RepositoryFactoryOptions();
            configuration.Invoke(options);
            
            switch (options.DataStorageType)
            {
                case DataStorageTypes.File:
                    return services.AddSingleton<IRepositoryFactory>(provider =>
                    {
                        var env = provider.GetRequiredService<IHostingEnvironment>();
                        var dataDirectory = Path.Combine(
                            env.ContentRootPath,
                            options.FileStorageRootPath);
                        return new CustomRepositoryFactory(new FileDataProvider(dataDirectory));
                    });
                    
                case DataStorageTypes.DistributedCache:
                    return services.AddSingleton<IRepositoryFactory>(provider =>
                    {
                        var cache = provider.GetRequiredService<IDistributedCache>();
                        return new CustomRepositoryFactory(new DistCacheDataProvider(cache));
                    });

                case DataStorageTypes.EFCore:
                    throw new DataAccessResolveException("Data storge type 'EFCore' has not yet been implemented!");

                default:
                    throw new DataAccessResolveException("Unknown data storage type");
            }

        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            IRepositoryFactory repoFactory = services.BuildServiceProvider().GetService<IRepositoryFactory>();
            if (repoFactory == null)
                throw new DataAccessResolveException("IRepositoryFactory must be resolved before repositories!");

            services.AddTransient(provider => repoFactory.CreateCustomersRepository());

            return services;
        }

        public static IServiceCollection AddApplicationLoginServices(this IServiceCollection services)
        {
            services.AddTransient<ISummaryByCityReport, SummaryByCityReport>();

            return services;
        }
    }
}
