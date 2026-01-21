using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Api.Core;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Domain.Models;
using System.Net;

namespace SFA.DAS.Campaign.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterCampaignInterestController(ILogger<RegisterCampaignInterestController> logger) : ControllerBase
{
    // POST: RegisterCampaignInterest/RegisterInterest
    [HttpPost]
    [Route($"~/{RouteNames.RegisterInterest}")]
    [ProducesResponseType(typeof(UserData), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterInterest([FromServices] IUserDataRepository repository, [FromBody] UserData userData, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Register Campaign Interest API: Received request to add user details to campaign");

            if (userData == null)
            {
                logger.LogWarning("User details for registering interest is empty");
                return BadRequest(new { message = "User details for registering interest cannot be empty" });
            }

            var userDataEntity = new UserDataEntity
            {
                Id = userData.Id,
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

            var result = await repository.AddNewCampaignInterestAsync(userDataEntity, cancellationToken);

            if (result != null && result.Entity != null)
            {
                return CreatedAtAction(nameof(RegisterInterest), new { id = result.Entity.Id }, result.Entity);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to Register Campaign Interest : An error occurred");
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}

