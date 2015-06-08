using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace SeleniumTest
{
    class HomePage
    {
        private IWebDriver driver;
        private static string title = "ONLINE STORE | Toolsqa Dummy Test site"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// HomePage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return string.Equals(d.Title, title, StringComparison.CurrentCultureIgnoreCase); });
            
            if (!string.Equals(driver.Title, title, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new NotFoundException("This is not the home page:" + driver.Title);
            }
        }

        /// <summary>
        /// Enter the given string into the product search field and hit Enter
        /// </summary>
        /// <param name="productName">The string to enter</param>
        /// <returns>The SearchResults page object</returns>
        public SearchResultsPage TypeProductSearch(string productName)
        {
            driver.FindElement(By.Name("s")).SendKeys(productName + Keys.Enter);
            return new SearchResultsPage(driver);
        }

        /// <summary>
        /// Click on the My Account icon to navigate to the account page
        /// </summary>
        /// <returns>The resulting AccountPage object</returns>
        public AccountPage NavigateToAccountPage()
        {
            //driver.FindElement(By.XPath("//*[@id='account']/a")).Click();
            //driver.FindElement(By.ClassName("account_icon")).Click();

            //this is really troublesome in IE, sometimes it works and othertimes not; I haven't determined a pattern yet
            //works like a charm in FireFox
            driver.FindElement(By.ClassName("account_icon")).FindElement(By.ClassName("icon")).Click();
            return new AccountPage(driver);
        }

        /// <summary>
        /// If the logout link is shown, clicks on it
        /// Otherwise do nothing
        /// </summary>
        /// <returns>Current HomePage object</returns>
        public HomePage Logout()
        {
            IWebElement logout;

            try
            {
                logout = driver.FindElement(By.LinkText("(Logout)"));
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return this;
            }

            logout.Click();
            return this;
        }
    }
}
