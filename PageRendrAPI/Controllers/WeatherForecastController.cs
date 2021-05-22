using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageRendrAPI.Controllers
{
    [ApiController]
    [Route("gamer")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string gamer = "https://silverdimond.tk")
        {
            var browser = new SeleniumBrowser();
            return File(await browser.RenderUrlAsyncAsByteArray(gamer), "image/png");
        }
    }
}