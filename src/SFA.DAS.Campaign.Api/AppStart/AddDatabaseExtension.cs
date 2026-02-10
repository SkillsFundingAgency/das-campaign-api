using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.Campaign.Api.Data;
using SFA.DAS.Campaign.Api.Domain.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api.AppStart;

[ExcludeFromCodeCoverage]
public static class AddDatabaseExtension
{
    public static void AddDatabaseRegistration(this IServiceCollection services, CampaignConfiguration config, string environmentName)
    {
        if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<CampaignDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.Campaign.Api"), ServiceLifetime.Transient);
        }
        else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<CampaignDataContext>(options => options.UseSqlServer(config.SqlConnectionString), ServiceLifetime.Transient);
        }
        else
        {
            services.AddSingleton(new AzureServiceTokenProvider());
            services.AddDbContext<CampaignDataContext>(ServiceLifetime.Transient);
        }

        services.AddTransient<ICampaignDataContext, CampaignDataContext>(provider => provider.GetRequiredService<CampaignDataContext>());
        services.AddTransient(provider => new Lazy<CampaignDataContext>(provider.GetRequiredService<CampaignDataContext>()));
    }
}
