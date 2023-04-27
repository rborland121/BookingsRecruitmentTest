using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZonalTechTest.Application;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Repository;

namespace ZonalTechTest.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LaunchController : ControllerBase
{
    private readonly ILaunchBL _launchBl;

    public LaunchController(ILaunchBL launchBl)
    {
        _launchBl = launchBl;
    }

    [HttpGet]
    public async Task<LaunchDTO?> GetLaunch([FromQuery] int flightNumber)
        => await _launchBl.GetLaunchAsync(flightNumber);

    [HttpGet]
    public async Task<IEnumerable<LaunchDTO>> GetAllLaunches()
        => await _launchBl.GetLaunchesAsync();


    [HttpPost("{flightNumber}")]
    public async Task<IActionResult> AddOrUpdate(int flightNumber)
    {
        bool success = await _launchBl.AddLaunchAsync(flightNumber);

        return success ? NoContent() : BadRequest("Couldn't find SpaceX flight: " + flightNumber);
    }

    [HttpDelete("{flightNumber}")]
    public async Task<NoContentResult> Delete(int flightNumber)
    {
        await _launchBl.DeleteLaunchAsync(flightNumber);

        return NoContent();
    }
}
