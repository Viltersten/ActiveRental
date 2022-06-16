namespace Api.Controllers;

// [Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    public AdminController(IAdminService service) => Service = service;

    private IAdminService Service { get; }

    [HttpPost("pickup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PickupInfo>> PickupAsync(PickupDto payload)
    {
        PickupInfo output = await Service.RegisterPickupAsync(payload);

        return Ok(output);
    }

    [HttpPost("return")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ReturnInfo>> ReturnAsync(ReturnDto payload)
    {
        ReturnInfo output = await Service.RegisterReturnAsync(payload);

        return Ok(output);
    }
}