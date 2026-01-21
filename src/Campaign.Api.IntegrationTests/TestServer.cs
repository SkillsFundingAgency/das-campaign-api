using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.Encoding;
using SFA.DAS.Campaign.Api.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SFA.DAS.Campaign.Api.IntegrationTests;

public class TestServer : WebApplicationFactory<Program>
{
    public Mock<ICampaignDataContext> DataContext { get; } = new ();
    public Mock<IEncodingService> EncodingService { get; } = new ();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder
            .ConfigureHostConfiguration(configBuilder => configBuilder.AddJsonFile("appsettings.Test.json"))
            .ConfigureAppConfiguration(configBuilder => configBuilder.SetBasePath(Directory.GetCurrentDirectory()))
            .ConfigureServices(services =>
            {
                services.AddTransient<ICampaignDataContext>(x => DataContext.Object);
                services.AddTransient<IEncodingService>(x => EncodingService.Object);
            });
        
        return base.CreateHost(builder);
    }
}