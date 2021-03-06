using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V93.Network;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SDBrowser
{
    public sealed class SeleniumBrowser : IBrowser
    {
        private readonly IWebDriver _webDriver;
        private bool _isLocked;

        public SeleniumBrowser(IWebDriver driver) => _webDriver = driver;

        public SeleniumBrowser()
        {
            var chromeOptions = new ChromeOptions();
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_CHROME_BIN")))
            {
                chromeOptions.BinaryLocation = Environment.GetEnvironmentVariable("GOOGLE_CHROME_BIN");
            }
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("disable-gpu");
            chromeOptions.AddArguments("no-sandbox");
            chromeOptions.AddArguments("disable-dev-shm-usage");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("dns-prefetch-disable");
            _webDriver = new ChromeDriver(chromeOptions);
            _webDriver.Manage().Window.Size = new Size(1920, 1080);
        }
        public void BlockUrlsUsingChromeDevTools(string[] urls)
        {
            var devTools = (_webDriver as IDevTools).GetDevToolsSession();
            SetBlockedURLsCommandSettings blockedUrlSettings = new SetBlockedURLsCommandSettings
            {
                Urls = urls
            };
            devTools.SendCommand(blockedUrlSettings);
            devTools.SendCommand(new EnableCommandSettings());
        }
        public async Task<Bitmap> RenderHtmlAsync(string html, byte waittime = 0)
        {
            return Utils.ByteArrayToImage(await RenderHtmlAsyncAsByteArray(html, waittime));
        }

        public async Task<byte[]> RenderHtmlAsyncAsByteArray(string html, byte waittime = 0)
        {
            while (_isLocked)
            {
                await Task.Delay(500);
            }
            _isLocked = true;
            _webDriver.Url = "data:text/html;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(html));
            _webDriver.Navigate();
            IWait<IWebDriver> wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(driver1 => ((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.readyState").Equals("complete"));
            if (waittime != 0)
            {
                await Task.Delay(waittime * 1000);
            }
            var ss = ((ITakesScreenshot)_webDriver).GetScreenshot();
            _isLocked = false;
            return ss.AsByteArray;
        }

        public async Task<Bitmap> RenderUrlAsync(string url, byte waittime = 0)
        {
            return Utils.ByteArrayToImage(await RenderUrlAsyncAsByteArray(url, waittime));
        }

       

        public async Task<byte[]> RenderUrlAsyncAsByteArray(string url, byte waittime = 0)
        {
            while (_isLocked)
            {
                await Task.Delay(500);
            }
            _isLocked = true;
            try
            {
                _webDriver.Url = url;
                _webDriver.Navigate();
                IWait<IWebDriver> wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
                wait.Until(driver1 => ((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.readyState").Equals("complete"));
                if(waittime != 0)
                {
                    await Task.Delay(waittime * 1000);
                }
                var ss = ((ITakesScreenshot)_webDriver).GetScreenshot();
                _isLocked = false;
                return ss.AsByteArray;
            }
            catch (Exception)
            {
                _isLocked = false;
                throw;
            }
        }
    }
}