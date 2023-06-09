﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZonalTechTest.Application;
using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RocketController : ControllerBase
    {
        private readonly IRocketBL _rocketBl;

        public RocketController(IRocketBL rocketBl)
        {
            _rocketBl = rocketBl;
        }

        [HttpGet]
        public async Task<RocketDTO?> GetRocket([FromQuery] string rocketId)
            => await _rocketBl.GetRocketAsync(rocketId);

        [HttpGet]
        public async Task<IEnumerable<RocketDTO>> GetAllRockets()
            => await _rocketBl.GetAllRocketsAsync();
    }
}
