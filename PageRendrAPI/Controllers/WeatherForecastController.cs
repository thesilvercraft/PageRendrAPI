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
    [Route("renderpage")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string url = "https://silverdimond.tk")
        {
            var browser = new SeleniumBrowser();
            return File(await browser.RenderUrlAsyncAsByteArray(url), "image/png");
        }
    }
}