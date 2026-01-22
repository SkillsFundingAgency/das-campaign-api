using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Campaign.Api.Data.Repositories;
using SFA.DAS.Campaign.Api.Domain.Entities;
using System.Net;

namespace SFA.DAS.Campaign.Api.Controllers;

[ApiController]
[Route("api/registercampaigninterest")]
public class RegisterCampaignInterestController(ILogger<RegisterCampaignInterestController> logger) : ControllerBase
{
    // POST: api/registercampaigninterest/registerinterest
    [HttpPost("registerinterest")]
    [ProducesResponseType(typeof(UserDataEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterInterest([FromServices] IUserDataRepository repository, [FromBody] UserDataEntity userData, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Register Campaign Interest API: Received request to add user details to campaign");

            if (userData == null)
            {
                logger.LogWarning("User details for registering interest is empty");
                return BadRequest(new { message = "User details for registering interest cannot be empty" });
            }

            var result = await repository.AddNewCampaignInterestAsync(userData, cancellationToken);

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

