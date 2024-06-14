using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Services;
using Asset.Management.Infra.Repository;
using Microsoft.AspNetCore.Builder;

namespace Asset.Management.API.Configuration;

public class BuilderServiceConfiguration
{

}

public static class BuilderServiceConfigurationExtensions
{
    public static void ConfigurationDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IProductService, ProductService>();
    }
}