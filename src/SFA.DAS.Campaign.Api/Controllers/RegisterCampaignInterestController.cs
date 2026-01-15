using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.AODP.Application;
using SFA.DAS.AODP.Application.Commands.FormBuilder.Forms;
using SFA.DAS.AODP.Application.Queries.Jobs;
using SFA.DAS.AODP.Data.Enum;

namespace SFA.DAS.AODP.Api.Controllers.Jobs;

[ApiController]
[Route("api/[controller]")]
public class RegisterCampaignInterestController : BaseController
{
    private readonly ILogger<RegisterCampaignInterestController> _logger;

    public RegisterCampaignInterestController(ILogger<RegisterCampaignInterestController> logger) : base(logger)
    {
        _logger = logger;
    }

    [HttpPost("/api/job/request-run")]
    [ProducesResponseType(typeof(EmptyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] RequestJobRunCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.JobName))
        {
            _logger.LogWarning("Job name is empty");
            return BadRequest(new { message = "Job name cannot be empty" });
        }

        return await SendRequestAsync(command);
    }
}

