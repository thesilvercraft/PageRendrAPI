using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PageRendrAPI.Controllers
{
    [ApiController]
    [Route("renderpage")]   
    public class RenderController : ControllerBase
    {
        private readonly SeleniumBrowser Browser;
        private readonly HttpClient HttpClient;
        private readonly ApiClient ApiClient;

        public RenderController(SeleniumBrowser browser, HttpClient httpClient, ApiClient apiClient)
        {
            Browser = browser;
            HttpClient = httpClient;
            ApiClient = apiClient;
        }

        [HttpGet]
        [ClientIPAddressFilter]
        public async Task<IActionResult> Get(string url = "https://silverdimond.tk")
        {
            var imageEndpoint = new ImageEndpoint(ApiClient, HttpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(new MemoryStream(await Browser.RenderUrlAsyncAsByteArray(url)));
            return Redirect(imageUpload.Link);
        }
    }
}