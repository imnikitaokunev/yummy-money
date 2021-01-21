using System.Collections.Generic;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Core;
using CostAccounting.Services.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Angular.Controllers
{
    [Route("api/incomes/")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService) => _incomeService = incomeService;

        [HttpGet("")]
        public ActionResult<IEnumerable<IncomeDto>> Get([FromQuery] IncomeRequestModel request) =>
            Ok(_incomeService.Get(request));
    }
}