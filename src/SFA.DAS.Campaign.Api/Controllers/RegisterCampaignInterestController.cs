using Ganss.Xss;
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

            UserData userData = new()
            {
                FirstName = InputSanitizer.Clean(userDataEntity.FirstName),
                LastName = InputSanitizer.Clean(userDataEntity.LastName),
                Email = InputSanitizer.Clean(userDataEntity.Email),
                UkEmployerSize = InputSanitizer.Clean(userDataEntity.UkEmployerSize),
                PrimaryIndustry = InputSanitizer.Clean(userDataEntity.PrimaryIndustry),
                PrimaryLocation = InputSanitizer.Clean(userDataEntity.PrimaryLocation),
                AppsgovSignUpDate = userDataEntity.AppsgovSignUpDate,
                PersonOrigin = InputSanitizer.Clean(userDataEntity.PersonOrigin),
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

public static class InputSanitizer
{
    private static readonly HtmlSanitizer _sanitizer = new();

    public static string Clean(string input) => string.IsNullOrWhiteSpace(input) ? input : _sanitizer.Sanitize(input.Trim());
}