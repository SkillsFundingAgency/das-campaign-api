namespace SFA.DAS.Campaign.Api.Data;

public abstract class CampaignDataException(string? message, string? detail): Exception(message)
{
    public string? Detail => detail;
}