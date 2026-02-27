using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Entities;
using SFA.DAS.Campaign.Api.Domain.Models;
using System.Net;

namespace SFA.DAS.Campaign.Api.Controllers;

[ApiController]
[Route("api/registercampaigninterest")]
public class RegisterCampaignInterestController(ILogger<RegisterCampaignInterestController> logger) : ControllerBase
{
    // POST: api/registercampaigninterest/registerinterest
    [HttpPost("registerinterest")]
    [ProducesResponseType(typeof(UserDataEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserDataEntity), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterInterest([FromServices] IUserDataRepository repository, [FromBody] UserDataEntity userDataEntity, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Register Campaign Interest API: Received request to add user details to campaign");

            if (userDataEntity == null)
            {
                logger.LogWarning("User details for registering interest is empty");
                return BadRequest(new { message = "User details for registering interest cannot be empty" });
            }

            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid data received");
                return BadRequest(ModelState);
            }

            UserData userData = new()
            {
                FirstName = userDataEntity.FirstName,
                LastName = userDataEntity.LastName,
                Email = userDataEntity.Email,
                UkEmployerSize = userDataEntity.UkEmployerSize,
                PrimaryIndustry = userDataEntity.PrimaryIndustry,
                PrimaryLocation = userDataEntity.PrimaryLocation,
                AppsgovSignUpDate = userDataEntity.AppsgovSignUpDate,
                PersonOrigin = userDataEntity.PersonOrigin,
                IncludeInUR = userDataEntity.IncludeInUR
            };

            logger.LogInformation("Register Campaign Interest API: Adding new campaign interest for user with Email: {Email}", userData.Email);
            var result = await repository.AddNewCampaignInterestAsync(userData, cancellationToken);

            if (result != null && result.Entity != null)
            {
                logger.LogInformation("Successfully registered campaign interest for user with Id: {UserId}", result.Entity.Id);
                return CreatedAtAction(nameof(RegisterInterest), new { id = result.Entity.Id }, result.Entity);
            }
            else
            {
                logger.LogError("Unable to Register Campaign Interest : An error occurred while processing the request");
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
