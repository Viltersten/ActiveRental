using System.Text.Json;

namespace Api.Controllers;

//[Authorize(Policy = "agent")]
[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    [HttpGet("test-pickup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> TestPickupAsync()
    {
        PickupDto data = new() { CustomerId = "7602081114", Mileage = 4300, Plate = "abc123" };
        JsonContent content = JsonContent.Create(data);

        PickupInfo test1 = new() { Id = Guid.NewGuid() };
        string test2 = JsonSerializer.Serialize(test1).Replace("Id", "id");
        PickupInfo test3 = JsonSerializer.Deserialize<PickupInfo>(test2);
        PickupInfo? test4 = JsonSerializer.Deserialize<PickupInfo>(test2);
        PickupInfo? test5 = JsonSerializer.Deserialize<PickupInfo?>(test2);
        PickupInfo? test6 = test2.Convert<PickupInfo>();

        HttpResponseMessage result = await Http.PostAsync("pickup", content);
        string payload = await result.Content.ReadAsStringAsync();
        PickupInfo? output = payload.Convert<PickupInfo>();

        if (result.IsSuccessStatusCode)
            return Ok(output);
        return BadRequest();
    }

    [HttpGet("test-return")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> TestReturnAsync(Guid id)
    {
        ReturnDto data = new() { Id = id, Mileage = 4800 };
        JsonContent content = JsonContent.Create(data);

        HttpResponseMessage result = await Http.PostAsync("return", content);
        string payload = await result.Content.ReadAsStringAsync();
        ReturnInfo? output = payload.Convert<ReturnInfo>();

        if (result.IsSuccessStatusCode)
            return Ok(output);
        return BadRequest();
    }

    static HttpClient Http => new() { BaseAddress = new Uri("https://localhost:7042/admin/") };
}