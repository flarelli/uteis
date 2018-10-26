using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Helpers
{
    public class DriverFactory
    {
        public static IWebDriver INSTANCE { set; get; } = null;

        public static void InitializeDriver(string browser)
        {
            var abc = INSTANCE;

            if (INSTANCE == null)
            {
                if (browser.Equals("Internet Explorer") || browser.Equals("internet explorer") || browser.Equals("IE") || browser.Equals("ie") || browser.Equals("Microsoft Internet Explorer") || browser.Equals("microsoft internet explorer"))
                {
                    var options = new InternetExplorerOptions()
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        IgnoreZoomLevel = true,
                        EnableNativeEvents = false
                    };

                    INSTANCE = new InternetExplorerDriver(options);
                    INSTANCE.Manage().Window.Maximize();
                }

                if (browser.Equals("Chrome") || browser.Equals("Google Chrome") || browser.Equals("chrome") || browser.Equals("google chrome") || browser.Equals("CHROME"))
                {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("e.default_directory", AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Downloads"));
                    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

                    INSTANCE = new ChromeDriver(chromeOptions);
                    INSTANCE.Manage().Window.Maximize();                    
                }
            }
        }

        public static void CloseDriver()
        {
            if (INSTANCE != null)
            {
                INSTANCE.Quit();
                INSTANCE = null;
            }
        }

        public static string ArmazenaPagina()
        {
            string paginaatual = INSTANCE.Url;
            return paginaatual;
        }
    }
}
