namespace SFA.DAS.Campaign.Api.Core;

internal struct RouteElements
{
    public const string Api = "api";
    public const string RegisterCampaignInterest = "registerinterest";
}

public struct RouteNames
{
    public const string RegisterInterest = $"{RouteElements.Api}/{RouteElements.RegisterCampaignInterest}";
}