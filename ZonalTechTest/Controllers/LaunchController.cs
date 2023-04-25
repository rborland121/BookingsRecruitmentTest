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
    private ILaunchBL _launchBL;
    public LaunchController(ILaunchBL launchBL)
    {
        _launchBL = launchBL;
    }

    [HttpGet]
    public async Task<IEnumerable<LaunchDTO>> GetLaunch([FromQuery] int flightNumber)
        => await _launchBL.GetLaunchAsync(flightNumber);
    

    [HttpPost("/{flightNumber}")]
    public async Task<NoContentResult> AddLaunch(int flightNumber)
    {
        _launchBL.AddLaunchAsync(flightNumber);

        return NoContent();
    }
}
