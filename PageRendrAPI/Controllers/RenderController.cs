using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static readonly Dictionary<string, Tuple<string, DateTime>> Cache = new();

        [HttpGet]
        [ClientIPAddressFilter]
        public async Task<IActionResult> Get(string url = "https://silverdiamond.cf",bool waitfor10s=false)
        {
            Debug.WriteLine(Cache.Count == 1 ? "There is {0} thing in cache" : "There are {0} things in the cache", Cache.Count);
            if (Cache.TryGetValue(url, out var thing))
            {
                Debug.WriteLine("Cache is {0} old", DateTime.Now - thing.Item2);
                if ((DateTime.Now - thing.Item2) < TimeSpan.FromMinutes(10))
                {
                    Debug.WriteLine("Used url from cache");
                    return Redirect(thing.Item1);
                }
            }
            var imageEndpoint = new ImageEndpoint(ApiClient, HttpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(new MemoryStream(await Browser.RenderUrlAsyncAsByteArray(url, waitfor10s)));
            if (Cache.ContainsKey(url))
            {
                Cache.Remove(url);
            }
            Cache.Add(url, new Tuple<string, DateTime>(imageUpload.Link, DateTime.Now));
            return Redirect(imageUpload.Link);
        }
    }
}