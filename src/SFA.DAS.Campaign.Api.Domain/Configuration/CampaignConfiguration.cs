using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api.Domain.Configuration;

[ExcludeFromCodeCoverage]
public class CampaignConfiguration
{
    public required string SqlConnectionString { get; set; }
}