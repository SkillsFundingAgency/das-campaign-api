using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.Campaign.Api.Data;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Configuration;
using SFA.DAS.Encoding;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api.AppStart;

[ExcludeFromCodeCoverage]
public static class AddServiceRegistrationExtension
{
    public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // validators
        services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

        // repositories
        services.AddScoped<IUserDataRepository, UserDataRepository>();
    }

    public static void AddDatabaseRegistration(this IServiceCollection services, ConnectionStrings config, string? environmentName)
    {
        services.AddHttpContextAccessor();

        if (string.Equals(environmentName, "DEV", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<CampaigntDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.Campaign.Api"), ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<CampaigntDataContext>(options => options.UseSqlServer(config.SqlConnectionString), ServiceLifetime.Transient);
        }

        services.AddScoped<ICampaigntDataContext, CampaigntDataContext>(provider => provider.GetRequiredService<CampaigntDataContext>());
        services.AddScoped(provider => new Lazy<CampaigntDataContext>(provider.GetRequiredService<CampaigntDataContext>));
    }

    public static void ConfigureHealthChecks(this IServiceCollection services)
    {
        // health checks
        services.AddHealthChecks()
                .AddCheck<DefaultHealthCheck>("default");
    }

    public static void RegisterDasEncodingService(this IServiceCollection services, IConfiguration configuration)
    {
        var dasEncodingConfig = new EncodingConfig { Encodings = [] };
        configuration.GetSection(nameof(dasEncodingConfig.Encodings)).Bind(dasEncodingConfig.Encodings);
        services.AddSingleton(dasEncodingConfig);
        services.AddSingleton<IEncodingService, EncodingService>();
    }
}