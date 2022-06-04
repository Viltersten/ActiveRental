namespace Api.Services.Interfaces;

public interface IAdminService
{
    Task<PickupInfo> RegisterPickupAsync(PickupDto payload);
    Task<ReturnInfo> RegisterReturnAsync(ReturnDto payload);
}