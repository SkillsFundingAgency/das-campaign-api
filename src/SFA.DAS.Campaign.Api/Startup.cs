using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SFA.DAS.Api.Common.AppStart;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.Campaign.Api.AppStart;
using SFA.DAS.Campaign.Api.Data;
using SFA.DAS.Campaign.Api.Domain.Configuration;
using SFA.DAS.Campaign.Api.Domain.Models;
using SFA.DAS.Campaign.Api.Filters;
using SFA.DAS.Configuration.AzureTableStorage;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SFA.DAS.Campaign.Api;

[ExcludeFromCodeCoverage]
internal class Startup
{
    private readonly string _environmentName;
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        _environmentName = configuration["EnvironmentName"]!;

        if (_environmentName == "INTEGRATION")
        {
            Configuration = configuration;
            return;
        }

        var config = new ConfigurationBuilder()
            .AddConfiguration(configuration)
            .AddAzureTableStorage(options =>
                {
                    options.ConfigurationNameIncludesVersionNumber = true;
                    options.ConfigurationKeys = configuration["ConfigNames"]!.Split(",");
                    options.EnvironmentName = _environmentName;
                    options.PreFixConfigurationKeys = false;
                    options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                });

#if DEBUG
        config.AddJsonFile("appsettings.Development.json", true);
        config.AddJsonFile("appsettings.json", true);
#endif
        Configuration = config.Build();
    }

    private bool IsEnvironmentLocalOrDev =>
        _environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase)
        || _environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)
        || _environmentName.Equals("TEST", StringComparison.CurrentCultureIgnoreCase);

    public void ConfigureServices(IServiceCollection services)
    {
        if (!IsEnvironmentLocalOrDev)
        {
            var azureAdConfiguration = Configuration.GetSection("AzureAd").Get<AzureActiveDirectoryConfiguration>();

            var policies = new Dictionary<string, string>
            {
                { PolicyNames.Default, "Default" },
            };
            services.AddAuthentication(azureAdConfiguration, policies);
            services.AddHealthChecks()
                    .AddDbContextCheck<CampaignDataContext>();
        }

        services.Configure<CampaignConfiguration>(Configuration.GetSection(nameof(CampaignConfiguration)));
        services.AddSingleton(cfg => cfg.GetService<IOptions<CampaignConfiguration>>()!.Value);
        var candidateAccountConfiguration = Configuration.GetSection(nameof(CampaignConfiguration)).Get<CampaignConfiguration>();

        services.AddMvc(o =>
            {
                if (!IsEnvironmentLocalOrDev)
                {
                    o.Conventions.Add(new AuthorizeControllerModelConvention(new List<string> { Capacity = 0 }));
                }
                o.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
            })
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        services.RegisterDasEncodingService(Configuration);
        services.AddApplicationDependencies(Configuration);
        services.AddDatabaseRegistration(candidateAccountConfiguration!, Configuration["EnvironmentName"]);
        services.AddOpenTelemetryRegistration(Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]!);
        services.ConfigureHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recruit.Api", Version = "v1" });
            c.OperationFilter<SwaggerVersionHeaderFilter>();
            c.DocumentFilter<JsonPatchDocumentFilter>();
            c.DocumentFilter<HealthChecksFilter>();
            c.MapType<UserData>(() => new OpenApiSchema { Type = "string" });
        });
        services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseAuthentication();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SFA.DAS.Campaign.Api v1");
                options.RoutePrefix = string.Empty;
            });
        app.UseHealthChecks();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}