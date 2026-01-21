using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.Campaign.Api.Controllers;
using SFA.DAS.Campaign.Api.Data.Models;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.Campaign.Api.UnitTests.Controllers;

internal class RegisterCampainInterestControllerTests
{
    // unit test to cover RegisterCampaignInterest controller RegisterInterest method when user is created
    private static readonly UserData userData = new()
    {
        FirstName = "John",
        LastName = "Doe",
        Email = "John.Doe@TestEmail.com",
        UkEmployerSize = 10,
        PrimaryIndustry = "IT",
        PrimaryLocation = "London",
        AppsgovSignUpDate = DateTime.UtcNow,
        PersonOrigin = "Test",
        IncludeInUR = true
    };

    private static readonly UserDataEntity userDataEntity = new()
    {
        FirstName = userData.FirstName,
        LastName = userData.LastName,
        Email = userData.Email,
        UkEmployerSize = userData.UkEmployerSize,
        PrimaryIndustry = userData.PrimaryIndustry,
        PrimaryLocation = userData.PrimaryLocation,
        AppsgovSignUpDate = userData.AppsgovSignUpDate,
        PersonOrigin = userData.PersonOrigin,
        IncludeInUR = userData.IncludeInUR
    };

    [Test, RecursiveMoqAutoData]
    public async Task Then_The_User_Is_Created(Mock<IUserDataRepository> repository, [Greedy] RegisterCampaignInterestController sut, CancellationToken token)
    {
        // arrange
        repository.Setup(x => x.AddNewCampaignInterestAsync(It.IsAny<UserDataEntity>(), token))
                  .ReturnsAsync(new UpsertResult<UserDataEntity>(userDataEntity, true));

        // act
        var result = await sut.RegisterInterest(repository.Object, userData, token);
        var createdResult = result as CreatedAtActionResult;

        // assert
        repository.Verify(
            x => x.AddNewCampaignInterestAsync(
                It.Is<UserDataEntity>(e =>
                    e.FirstName == userData.FirstName &&
                    e.LastName == userData.LastName &&
                    e.Email == userData.Email &&
                    e.UkEmployerSize == userData.UkEmployerSize &&
                    e.PrimaryIndustry == userData.PrimaryIndustry &&
                    e.PrimaryLocation == userData.PrimaryLocation &&
                    e.AppsgovSignUpDate == userData.AppsgovSignUpDate &&
                    e.PersonOrigin == userData.PersonOrigin &&
                    e.IncludeInUR == userData.IncludeInUR
                ),
                token
            ),
            Times.Once
        );
        createdResult.Should().NotBeNull();
        createdResult!.Value.Should().BeEquivalentTo(userData, options => options.ExcludingMissingMembers());
        createdResult.ActionName.Should().Be(nameof(RegisterCampaignInterestController.RegisterInterest));
    }







    //[Test, RecursiveMoqAutoData]
    //public async Task Then_The_User_Is_Created(
    //    Guid userId,
    //    Mock<IUserDataRepository> repository,
    //    UserDataEntity entity,
    //    [Greedy] RegisterCampaignInterestController sut,
    //    CancellationToken token)
    //{
    //    // arrange
    //    request.UserType = UserType.Employer;
    //    repository
    //        .Setup(x => x.UpsertOneAsync(It.IsAny<UserDataEntity>(), token))
    //        .ReturnsAsync(UpsertResult.Create(entity, true));

    //    // act
    //    var result = await sut.PutOne(repository.Object, userId, request, token);
    //    var createdResult = result as Created<PutUserResponse>;

    //    // assert
    //    repository.Verify(x => x.UpsertOneAsync(ItIs.EquivalentTo(request.ToDomain(userId)), token), Times.Once);
    //    createdResult.Should().NotBeNull();
    //    createdResult.Value.Should().BeEquivalentTo(entity, options => options.ExcludingMissingMembers());
    //    createdResult.Location.Should().Be($"/api/user/{entity.Id}");
    //}
}