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
        private readonly SeleniumBrowser browser = new();

        [HttpGet]
        public async Task<IActionResult> Get(string url = "https://silverdimond.tk")
        {
            return File(await browser.RenderUrlAsyncAsByteArray(url), "image/png");
        }
    }
}