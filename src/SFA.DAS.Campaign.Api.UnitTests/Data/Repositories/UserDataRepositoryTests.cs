using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.Campaign.Api.Data;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.UnitTests.Data.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.Api.UnitTests.Data.Repositories;

internal class UserDataRepositoryTests
{
    [Test, RecursiveMoqAutoData]
    public async Task AddNewCampaignInterestAsync_Inserts_New_Entity(
        UserDataEntity entity,
        [Frozen] Mock<ICampaignDataContext> context,
        [Greedy] UserDataRepository sut,
        CancellationToken token)
    {
        // arrange
        var dbSet = new List<UserDataEntity>().BuildDbSetMock();
        context.Setup(x => x.UserDataEntities).Returns(dbSet.Object);

        // act
        var result = await sut.AddNewCampaignInterestAsync(entity, token);

        // assert
        context.Verify(x => x.SaveChangesAsync(token), Times.Once);
        dbSet.Verify(x => x.AddAsync(entity, token), Times.Once);
        result.Created.Should().BeTrue();
    }
}