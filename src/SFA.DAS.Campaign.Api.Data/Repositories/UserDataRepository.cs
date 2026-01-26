using Microsoft.EntityFrameworkCore;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Data.Models;
using SFA.DAS.Campaign.Api.Domain.Models;

namespace SFA.DAS.Campaign.Api.Data.Repositories;

public interface IUserDataRepository
{
    Task<UserData?> GetLatestForEmailAsync(string emailAddress, CancellationToken cancellationToken);

    Task<List<UserData>> GetAllForEmailAsync(string emailAddress, CancellationToken cancellationToken);

    Task<UpsertResult<UserData>> AddNewCampaignInterestAsync(UserData userData, CancellationToken cancellationToken);
}

public class UserDataRepository(ICampaignDataContext dataContext) : IUserDataRepository
{
    public async Task<UpsertResult<UserData>> AddNewCampaignInterestAsync(UserData userData, CancellationToken cancellationToken)
    {
        try
        {
            await dataContext.UserData.AddAsync(userData, cancellationToken);
            await dataContext.SaveChangesAsync(cancellationToken);
            return UpsertResult.Create(userData, true);
        }
        catch
        {
            return UpsertResult.Create(userData, false);
        }
    }


    public async Task<List<UserData>> GetAllForEmailAsync(string emailAddress, CancellationToken cancellationToken)
    {
        return await dataContext.UserData.Where(x => x.Email == emailAddress).ToListAsync(cancellationToken);
    }

    public async Task<UserData?> GetLatestForEmailAsync(string emailAddress, CancellationToken cancellationToken)
    {
        return await dataContext.UserData
                        .Where(x => x.Email == emailAddress)
                        .OrderByDescending(p => p.AppsgovSignUpDate)
                        .FirstOrDefaultAsync(cancellationToken);
    }
}