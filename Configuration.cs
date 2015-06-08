using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    public class Configuration
    {
        /// <summary>
        /// Return the BrowserAgent string from the app.config file
        /// </summary>
        public string BrowserAgent
        {
            get { return ConfigurationManager.AppSettings["BrowserAgent"]; }
        }

        /// <summary>
        /// Instantiate and return the correct browser driver based on the app.config setting
        /// </summary>
        public IWebDriver BrowserDriver
        {
            get
            { 
                string browserString = ConfigurationManager.AppSettings["BrowserString"];
                IWebDriver driver;

                switch (browserString.ToUpper())
                {
                    case "IE":
                        driver = new InternetExplorerDriver();
                        break;
                    case "FIREFOX":
                        driver = new FirefoxDriver();
                        break;
                    case "CHROME":
                        driver = new ChromeDriver();
                        break;
                    default:
                        driver = new FirefoxDriver();
                        break;
                }
                driver.Manage().Timeouts().ImplicitlyWait(System.TimeSpan.FromSeconds(5));
                return driver;
            }
        }

        /// <summary>
        /// Return the test URL from the app.config file
        /// </summary>
        public string TestURL
        {
            get { return ConfigurationManager.AppSettings["TestURL"]; }
        }

    }
}
