using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlloyTestApp.Core.Interfaces.BusinessLogic;
using AlloyTestApp.Core.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlloyTestApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryByCityReportController : ControllerBase
    {
        private readonly ISummaryByCityReport _report;

        public SummaryByCityReportController(ISummaryByCityReport report)
        {
            _report = report;
        }

        [HttpGet]
        public async Task<IEnumerable<SummaryByCityReportItem>> Get()
        {
            return await _report.GetReportItemsAsync();
        }
    }
}