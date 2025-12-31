using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/charging")]
public class ChargingController : ControllerBase
{
    private readonly IChargingService _chargingService;

    public ChargingController(IChargingService chargingService)
    {
        _chargingService = chargingService;
    }

    [HttpPost("request")]
    public IActionResult RequestCharging(int carId, int percentage)
    {
        _chargingService.RequestCharging(carId, percentage);
        return Ok();
    }
}
