using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CostAccounting.Web.Angular.Controllers
{
    [Route("api/environment")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EnvironmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var environment = _configuration.GetValue<string>("Environment");

            return new ObjectResult(environment);
        }
    }
}
