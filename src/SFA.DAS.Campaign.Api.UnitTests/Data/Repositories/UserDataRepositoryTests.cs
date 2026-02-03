using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.Campaign.Api.Data;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Models;
using SFA.DAS.Campaign.Api.UnitTests.Data.DatabaseMock;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.Api.UnitTests.Data.Repositories;

internal class UserDataRepositoryTests
{
    [Test, RecursiveMoqAutoData]
    public async Task AddNewCampaignInterestAsync_Inserts_New_Entity(
        UserData entity,
        [Frozen] Mock<ICampaignDataContext> context,
        [Greedy] UserDataRepository sut,
        CancellationToken token)
    {
        // arrange
        var dbSet = new List<UserData>().BuildDbSetMock();
        context.Setup(x => x.UserData).Returns(dbSet.Object);

        // act
        var result = await sut.AddNewCampaignInterestAsync(entity, token);

        // assert
        context.Verify(x => x.SaveChangesAsync(token), Times.Once);
        dbSet.Verify(x => x.AddAsync(entity, token), Times.Once);
        result.Entity.Should().BeEquivalentTo(entity);
    }

    [Test, RecursiveMoqAutoData]
    public async Task AddNewCampaignInterestAsync_Returns_False_When_Exception_Thrown(
        UserData entity,
        [Frozen] Mock<ICampaignDataContext> context,
        [Greedy] UserDataRepository sut,
        CancellationToken token)
    {
        // arrange
        context.Setup(x => x.UserData).Throws<Exception>();

        // act
        var result = await sut.AddNewCampaignInterestAsync(entity, token);

        // assert
        context.Verify(x => x.SaveChangesAsync(token), Times.Never);
        result.Entity.Should().BeEquivalentTo(entity);
        result.Created.Should().BeFalse();
    }

    [Test, RecursiveMoqAutoData]
    public async Task AddNewCampaignInterestAsync_Returns_False_When_Entity_Not_Found_After_Insert(
        UserData entity,
        [Frozen] Mock<ICampaignDataContext> context,
        [Greedy] UserDataRepository sut,
        CancellationToken token)
    {
        // arrange
        var dbSet = new List<UserData>().BuildDbSetMock();
        context.Setup(x => x.UserData).Returns(dbSet.Object);

        // act
        var result = await sut.AddNewCampaignInterestAsync(entity, token);

        // assert
        context.Verify(x => x.SaveChangesAsync(token), Times.Once);
        dbSet.Verify(x => x.AddAsync(entity, token), Times.Once);
        result.Entity.Should().BeEquivalentTo(entity);
        result.Created.Should().BeFalse();
    }

    [Test, RecursiveMoqAutoData]
    public async Task AddNewCampaignInterestAsync_Returns_True_When_Entity_Found_After_Insert(
        UserData entity,
        [Frozen] Mock<ICampaignDataContext> context,
        [Greedy] UserDataRepository sut,
        CancellationToken token)
    {
        // arrange
        var dbSet = new List<UserData> { entity }.BuildDbSetMock();
        context.Setup(x => x.UserData).Returns(dbSet.Object);
        // act
        var result = await sut.AddNewCampaignInterestAsync(entity, token);
        // assert
        context.Verify(x => x.SaveChangesAsync(token), Times.Once);
        dbSet.Verify(x => x.AddAsync(entity, token), Times.Once);
        result.Entity.Should().BeEquivalentTo(entity);
        result.Created.Should().BeTrue();
    }

    [Test, RecursiveMoqAutoData]
    public async Task AddNewCampaignInterestAsync_Calls_AddAsync_With_Correct_Entity(
        UserData entity,
        [Frozen] Mock<ICampaignDataContext> context,
        [Greedy] UserDataRepository sut,
        CancellationToken token)
    {
        // arrange
        var dbSet = new List<UserData>().BuildDbSetMock();
        context.Setup(x => x.UserData).Returns(dbSet.Object);

        // act
        await sut.AddNewCampaignInterestAsync(entity, token);

        // assert
        dbSet.Verify(x => x.AddAsync(It.Is<UserData>(e =>
            e.Email == entity.Email &&
            e.AppsgovSignUpDate == entity.AppsgovSignUpDate &&
            e.FirstName == entity.FirstName &&
            e.LastName == entity.LastName), token), Times.Once);
    }
}