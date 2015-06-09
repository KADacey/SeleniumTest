using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class ProfilePage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Profile"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// ProfilePage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public ProfilePage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                throw new NotFoundException("This is not the Profile page: " + driver.Title);
            }
        }

        /// <summary>
        /// Enter the given string into the First Name field
        /// </summary>
        /// <param name="firstName">The string to enter</param>
        /// <returns>Current page object</returns>
        public ProfilePage TypeFirstName(string firstName)
        {
            IWebElement field = driver.FindElement(By.Id("first_name"));
            field.Clear();
            field.SendKeys(firstName);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Last name field
        /// </summary>
        /// <param name="lastName">THe string to enter</param>
        /// <returns>Current page object</returns>
        public ProfilePage TypeLastName(string lastName)
        {
            IWebElement field = driver.FindElement(By.Id("last_name"));
            field.Clear();
            field.SendKeys(lastName);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Nickname field
        /// </summary>
        /// <param name="nickname">The string to enter</param>
        /// <returns>Current page object</returns>
        public ProfilePage TypeNickname(string nickname)
        {
            IWebElement field = driver.FindElement(By.Id("nickname"));
            field.Clear();
            field.SendKeys(nickname);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Email field
        /// </summary>
        /// <param name="email">The string to enter</param>
        /// <returns>Current page object</returns>
        public ProfilePage TypeEmail(string email)
        {
            IWebElement field = driver.FindElement(By.Id("email"));
            field.Clear();
            field.SendKeys(email);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Password field
        /// </summary>
        /// <param name="pwd">The string to enter</param>
        /// <returns>Current page object</returns>
        public ProfilePage TypePassword(string pwd)
        {
            IWebElement field = driver.FindElement(By.Id("pass1"));
            field.Clear();
            field.SendKeys(pwd);
            return this;
        }

        /// <summary>
        /// Enter the given string into the password repeat field
        /// </summary>
        /// <param name="pwd">The string to enter</param>
        /// <returns>Current page object</returns>
        public ProfilePage TypePasswordRepeat(string pwd)
        {
            IWebElement field = driver.FindElement(By.Id("pass2"));
            field.Clear();
            field.SendKeys(pwd);
            return this;
        }

        /// <summary>
        /// Click on the Update Profile button
        /// </summary>
        /// <returns>Current page object</returns>
        public ProfilePage UpdateProfile()
        {
            driver.FindElement(By.Id("submit")).Click();
            return this;
        }

        /// <summary>
        /// Click on the link to return to the Home page
        /// </summary>
        /// <returns>The resulting Home page object</returns>
        public HomePage NavigateToHomePage()
        {
            driver.FindElement(By.LinkText("ONLINE STORE")).Click();
            return new HomePage(driver);
        }
    }
}
