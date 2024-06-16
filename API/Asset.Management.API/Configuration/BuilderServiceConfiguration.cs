using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Services;
using Asset.Management.Infra.Repository;

namespace Asset.Management.API.Configuration;


public static class BuilderServiceConfigurationExtensions
{
    public static void ConfigurationDI(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddSingleton<IProductRepository, ProductRepository>();
        serviceDescriptors.AddSingleton<IProductService, ProductService>();
        serviceDescriptors.AddSingleton<ITransactionService, TransactionService>();
        serviceDescriptors.AddSingleton<ITransactionRepository, TransactionRepository>();
    }

    public static void ConfigurationRedis(this IServiceCollection service, IConfiguration _config)
    {
        var redisConfig = _config.GetSection("redis"); 
        service.AddStackExchangeRedisCache(options => {
            options.Configuration = redisConfig["connection"];
        });
    }
}