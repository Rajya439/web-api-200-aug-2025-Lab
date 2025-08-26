

namespace HelpDesk.Api.Employee.Issues;

[ApiController]
public class EmployeeIssueController : ControllerBase
{
    [HttpPost("/employee/problems")]
    public async Task<ActionResult> AddEmployeeProblemAsync(
        [FromBody] SubmitIssueRequest request
        )
    {
        return Ok(request);
    }
}
