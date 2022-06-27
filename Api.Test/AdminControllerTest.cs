using Api.Controllers;
using Api.Models.Dtos;
using Api.Models.Infos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Test;

public class AdminControllerTest
{
    [Fact]
    public async Task RegisterPickUp()
    {
        PickupDto payload = new();
        Guid id = Guid.NewGuid();
        PickupInfo output = new() { Id = id };
        Mock<IAdminService> mockService = new();
        mockService.Setup(a => a.RegisterPickupAsync(payload))
            .ReturnsAsync(output);
        AdminController subject = new(mockService.Object);

        ActionResult<PickupInfo> outcome = await subject.PickupAsync(payload);
        OkObjectResult? result = outcome.Result as OkObjectResult;
        PickupInfo? info = result?.Value as PickupInfo;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
        Assert.Equal(id, info?.Id);
    }

    [Fact]
    public async Task RegisterReturn()
    {
        ReturnDto payload = new();
        Guid id = Guid.NewGuid();
        int charge = 123;
        ReturnInfo output = new() { Id = id, Charge = charge };
        Mock<IAdminService> mockService = new();
        mockService.Setup(a => a.RegisterReturnAsync(payload))
            .ReturnsAsync(output);
        AdminController subject = new(mockService.Object);

        ActionResult<ReturnInfo> outcome = await subject.ReturnAsync(payload);
        OkObjectResult? result = outcome.Result as OkObjectResult;
        ReturnInfo? info = result?.Value as ReturnInfo;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
        Assert.Equal(id, info?.Id);
        Assert.Equal(charge, info?.Charge);
    }
}