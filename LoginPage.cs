using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class LoginPage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Log In"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// LoginPage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1)            
            {
                throw new NotFoundException("This is not the Log In page: " + driver.Title);
            }
        }

        /// <summary>
        /// Enter the given string into the Username field
        /// </summary>
        /// <param name="userName">The string to enter</param>
        /// <returns>Current page object</returns>
        public LoginPage TypeUsername(string userName)
        {
            driver.FindElement(By.Id("user_login")).SendKeys(userName);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Password field
        /// </summary>
        /// <param name="password">The string to enter</param>
        /// <returns>Current page object</returns>
        public LoginPage TypePassword(string password)
        {
            driver.FindElement(By.Id("user_pass")).SendKeys(password);
            return this;
        }

        /// <summary>
        /// Click the login button
        /// </summary>
        /// <returns>The resulting Profile page object</returns>
        public ProfilePage ClickLoginButton()
        {
            driver.FindElement(By.Id("wp-submit")).Click();
            return new ProfilePage(driver);
        }
    }
}
