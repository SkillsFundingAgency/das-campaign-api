using Microsoft.EntityFrameworkCore;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Data.Models;

namespace SFA.DAS.Campaign.Api.Data.Repositories;

public interface IUserDataRepository
{
    Task<UserDataEntity?> GetLatestForEmailAsync(string emailAddress, CancellationToken cancellationToken);

    Task<List<UserDataEntity>> GetAllForEmailAsync(string emailAddress, CancellationToken cancellationToken);

    Task<UpsertResult<UserDataEntity>> AddNewCampaignInterestAsync(UserDataEntity userData, CancellationToken cancellationToken);
}

public class UserDataRepository(ICampaignDataContext dataContext) : IUserDataRepository
{
    public async Task<UpsertResult<UserDataEntity>> AddNewCampaignInterestAsync(UserDataEntity userData, CancellationToken cancellationToken)
    {
        try
        {
            await dataContext.UserDataEntities.AddAsync(userData, cancellationToken);
            await dataContext.SaveChangesAsync(cancellationToken);
            return UpsertResult.Create(userData, true);
        }
        catch
        {
            return UpsertResult.Create(userData, false);
        }
    }


    public async Task<List<UserDataEntity>> GetAllForEmailAsync(string emailAddress, CancellationToken cancellationToken)
    {
        return await dataContext.UserDataEntities.Where(x => x.Email == emailAddress).ToListAsync(cancellationToken);
    }

    public async Task<UserDataEntity?> GetLatestForEmailAsync(string emailAddress, CancellationToken cancellationToken)
    {
        return await dataContext.UserDataEntities
                        .Where(x => x.Email == emailAddress)
                        .OrderByDescending(p => p.AppsgovSignUpDate)
                        .FirstOrDefaultAsync(cancellationToken);
    }
}