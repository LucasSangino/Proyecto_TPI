using Dsw2025Ej15.Application.Services;
using Dsw2025Ej15.Data;
using Dsw2025Ej15.Data.Repositories;
using Dsw2025Ej15.Domain;
using Dsw2025Ej15.Domain.Entities;
using Dsw2025Ej15.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025Ej15.Api.Configurations;

public static class DomainServicesConfigurationExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Dsw2025Ej15Context>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Dsw2025Ej15Entities"));
            options.UseSeeding((c, t) =>
            {
                ((Dsw2025Ej15Context)c).Seedwork<Category>("Sources\\categories.json");
                ((Dsw2025Ej15Context)c).Seedwork<SubCategory>("Sources\\sub-categories.json");
                ((Dsw2025Ej15Context)c).Seedwork<Product>("Sources\\products.json");
            });
        });

        services.AddScoped<IRepository, EfRepository>();
        services.AddTransient<ProductsManagementService>();

        return services;
    }
}
