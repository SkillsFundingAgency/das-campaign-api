using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using SFA.DAS.Campaign.Api.Domain.Configuration;
using SFA.DAS.Campaign.Api.Domain.Models;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api.Data;

public interface ICampaignDataContext
{
    DbSet<UserData> UserData { get; }
    DatabaseFacade Database { get; }
    Task Ping(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void SetValues<TEntity>(TEntity to, TEntity from) where TEntity : class;
}

[ExcludeFromCodeCoverage]
public class CampaignDataContext : DbContext, ICampaignDataContext
{
    public DbSet<UserData> UserData { get; set; }

    private readonly CampaignConfiguration? _configuration;
    public CampaignDataContext() { }
    public CampaignDataContext(DbContextOptions options) : base(options) { }

    public CampaignDataContext(IOptions<CampaignConfiguration> config, DbContextOptions options) : base(options)
    {
        _configuration = config.Value;
    }

    public async Task Ping(CancellationToken cancellationToken)
    {
        await Database.ExecuteSqlRawAsync("SELECT 1;", cancellationToken).ConfigureAwait(false);
    }

    public void SetValues<TEntity>(TEntity to, TEntity from) where TEntity : class
    {
        Entry(to).CurrentValues.SetValues(from);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();

        if (_configuration == null)
        {
            optionsBuilder.UseSqlServer().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            return;
        }

        var connection = new SqlConnection { ConnectionString = _configuration!.SqlConnectionString, };
        optionsBuilder.UseSqlServer(connection, options =>
                        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(20), null)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        // Note: useful to keep here
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
        optionsBuilder.EnableDetailedErrors();
    }
}