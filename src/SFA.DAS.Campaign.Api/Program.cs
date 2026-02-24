using Microsoft.AspNetCore;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
               .ConfigureKestrel(c => c.AddServerHeader = false)
               .UseStartup<Startup>();
}
