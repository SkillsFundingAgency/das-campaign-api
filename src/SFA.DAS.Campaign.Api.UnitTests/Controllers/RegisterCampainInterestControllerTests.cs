using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SFA.DAS.Campaign.Api.Controllers;
using SFA.DAS.Campaign.Api.Data.Models;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Domain.Models;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.Campaign.Api.UnitTests.Controllers;

public class RegisterCampainInterestControllerTests
{
    // unit test to cover RegisterCampaignInterest controller RegisterInterest method when user is created
    private static readonly UserData userData = new()
    {
        FirstName = "John",
        LastName = "Doe",
        Email = "John.Doe@TestEmail.com",
        UkEmployerSize = "10",
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
        repository.Setup(x => x.AddNewCampaignInterestAsync(It.IsAny<UserData>(), token)).ReturnsAsync(new UpsertResult<UserData>(userData, true));

        // act
        var result = await sut.RegisterInterest(repository.Object, userDataEntity, token);
        var createdResult = result as CreatedAtActionResult;

        // assert
        repository.Verify(x => x.AddNewCampaignInterestAsync(
                It.Is<UserData>(e =>
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

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_UserData_Is_Null_BadRequest_Is_Returned([Greedy] RegisterCampaignInterestController sut, Mock<IUserDataRepository> repository, CancellationToken token)
    {
        // act
        var result = await sut.RegisterInterest(repository.Object, null!, token);
        var badRequestResult = result as BadRequestObjectResult;

        // assert
        badRequestResult.Should().NotBeNull();
        badRequestResult!.Value.Should().BeEquivalentTo(new { message = "User details for registering interest cannot be empty" });
    }

    [Test, RecursiveMoqAutoData]
    public async Task Then_If_Exception_Is_Thrown_InternalServerError_Is_Returned(Mock<IUserDataRepository> repository, [Greedy] RegisterCampaignInterestController sut, CancellationToken token)
    {
        // arrange
        repository.Setup(x => x.AddNewCampaignInterestAsync(It.IsAny<UserData>(), token)).ThrowsAsync(new Exception());

        // act
        var result = await sut.RegisterInterest(repository.Object, userDataEntity, token);

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>();
        result.As<StatusCodeResult>().StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
}