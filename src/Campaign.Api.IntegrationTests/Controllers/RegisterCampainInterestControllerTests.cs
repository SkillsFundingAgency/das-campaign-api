using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Api.Core;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Models.Requests;
using SFA.DAS.Recruit.Api.Models.Responses.User;
using System.Net;
using System.Net.Http.Json;

namespace SFA.DAS.Campaign.Api.IntegrationTests.Controllers;

public class RegisterCampainInterestControllerTests : BaseFixture
{
    [Test]
    public async Task Then_Without_Required_Fields_Bad_Request_Is_Returned()
    {
        // act
        var response = await Client.PutAsJsonAsync($"{RouteNames.RegisterInterest}", new { });
        var errors = await response.Content.ReadAsAsync<ValidationProblemDetails>();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().NotBeNull();
        errors.Errors.Should().HaveCount(3);
        errors.Errors.Should().ContainKeys(
            nameof(AddCampaignInterestRequest.FirstName),
            nameof(AddCampaignInterestRequest.LastName),
            nameof(AddCampaignInterestRequest.Email)
        );
    }

    [Test]
    public async Task Then_The_UserData_Is_Added()
    {
        // arrange
        var id = Guid.NewGuid();
        Server.DataContext.Setup(x => x.UserDataEntities).ReturnsDbSet([]);

        var request = Fixture.Create<AddCampaignInterestRequest>();
        var expectedEntity = request;

        // act
        var response = await Client.PutAsJsonAsync($"{RouteNames.RegisterInterest}", request);
        var user = await response.Content.ReadAsAsync<AddCampaignInterestResponse>();

        // assert
        user.Should().NotBeNull();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location.ToString().Should().Be($"/{RouteNames.RegisterInterest}");

        Server.DataContext.Verify(x => x.UserDataEntities.AddAsync(It.Is<UserDataEntity>(e => e.Equals(expectedEntity)), It.IsAny<CancellationToken>()), Times.Once());
        Server.DataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Then_The_UserData_Is_Replaced()
    {
        // arrange
        var items = Fixture.CreateMany<UserDataEntity>(10).ToList();
        var targetItem = items[5];
        Server.DataContext.Setup(x => x.UserDataEntities).ReturnsDbSet(items);

        Server.DataContext
            .Setup(x => x.UserDataEntities)
            .ReturnsDbSet([new UserDataEntity
                {
                    FirstName = targetItem.FirstName,
                    LastName = targetItem.LastName,
                    Email = targetItem.Email,
                    UkEmployerSize = targetItem.UkEmployerSize,
                    PrimaryIndustry = targetItem.PrimaryIndustry,
                    PrimaryLocation = targetItem.PrimaryLocation,
                    AppsgovSignUpDate = targetItem.AppsgovSignUpDate,
                    PersonOrigin = targetItem.PersonOrigin,
                    IncludeInUR = targetItem.IncludeInUR,
                }]);

        var request = Fixture.Create<AddCampaignInterestRequest>();

        // act
        var response = await Client.PutAsJsonAsync($"{RouteNames.RegisterInterest}", request);
        var user = await response.Content.ReadAsAsync<AddCampaignInterestResponse>();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        user.Should().NotBeNull();
        Server.DataContext.Verify(x => x.SetValues(targetItem, It.IsAny<UserDataEntity>()), Times.Once());
        Server.DataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


}