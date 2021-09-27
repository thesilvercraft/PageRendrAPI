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
        Dictionary<string, Tuple<string, DateTime>> Cache = new();

        [HttpGet]
        [ClientIPAddressFilter]
        public async Task<IActionResult> Get(string url = "https://silverdiamond.cf")
        {
            if(Cache.TryGetValue(url, out var thing))
            {
                if((DateTime.Now - thing.Item2 )<TimeSpan.FromMinutes(2))
                {
                    return Redirect(thing.Item1);
                }
            }
            var imageEndpoint = new ImageEndpoint(ApiClient, HttpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(new MemoryStream(await Browser.RenderUrlAsyncAsByteArray(url)));
            if(Cache.ContainsKey(url))
            {
                Cache.Remove(url);
            }
            Cache.Add(url, new Tuple<string, DateTime>(imageUpload.Link, DateTime.Now));
            return Redirect(imageUpload.Link);
        }
    }
}