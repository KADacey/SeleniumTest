using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class AccountPage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Your account"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// AccountPage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public AccountPage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1)            
            {
                throw new NotFoundException("This is not the Account page: " + driver.Title);
            }
        }

        #region InitialFields
        /// <summary>
        /// Enters the given string into the Username field
        /// </summary>
        /// <param name="userName">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeUsername(string userName)
        {
            IWebElement field = driver.FindElement(By.Id("log"));
            field.Clear();
            field.SendKeys(userName);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Password field
        /// </summary>
        /// <param name="password">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypePassword(string password)
        {
            IWebElement field = driver.FindElement(By.Id("pwd"));
            field.Clear();
            field.SendKeys(password);
            return this;
        }

        /// <summary>
        /// Finds and clicks on the Login button
        /// </summary>
        /// <returns>The current page object</returns>
        public AccountPage ClickLoginButton()
        {
            driver.FindElement(By.Id("login")).Click();
            return this;
        }
        #endregion

        #region LoggedInFields
        /// <summary>
        /// Finds and clicks on the Your Details link
        /// </summary>
        /// <returns>Current page object</returns>
        public AccountPage NavigateToYourDetails()
        {
            driver.FindElement(By.LinkText("Your Details")).Click();
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing First Name field
        /// </summary>
        /// <param name="firstName">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingFirstName(string firstName)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_2"));
            field.Clear();
            field.SendKeys(firstName);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing Last Name field
        /// </summary>
        /// <param name="lastName">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingLastName(string lastName)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_3"));
            field.Clear();
            field.SendKeys(lastName);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing Address field
        /// </summary>
        /// <param name="address">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingAddress(string address)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_4"));
            field.Clear();
            field.SendKeys(address);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing City field
        /// </summary>
        /// <param name="city">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingCity(string city)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_5"));
            field.Clear();
            field.SendKeys(city);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing Country field
        /// </summary>
        /// <param name="country">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingCountry(string country)
        {
            var countryDropdown = driver.FindElement(By.Id("wpsc_checkout_form_7"));
            var selectElement = new SelectElement(countryDropdown);

            selectElement.SelectByText(country);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing PostalCode field
        /// </summary>
        /// <param name="postalCode">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingPostalCode(string postalCode)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_8"));
            field.Clear();
            field.SendKeys(postalCode);
            return this;
        }

        /// <summary>
        /// Enters the given string into the Billing Phone field
        /// </summary>
        /// <param name="phone">The string to enter</param>
        /// <returns>The current page object</returns>
        public AccountPage TypeBillingPhone(string phone)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_18"));
            field.Clear();
            field.SendKeys(phone);
            return this;
        }

        /// <summary>
        /// Gets the current value of the Billing First Name field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingFirstName()
        {
            return driver.FindElement(By.Id("wpsc_checkout_form_2")).GetAttribute("value");
        }

        /// <summary>
        /// Gets the current value of the Billing Last Name field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingLastName()
        {
            return driver.FindElement(By.Id("wpsc_checkout_form_3")).GetAttribute("value");
        }

        /// <summary>
        /// Gets the current value of the Billing Address field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingAddress()
        {
            return driver.FindElement(By.Id("wpsc_checkout_form_4")).GetAttribute("value");
        }

        /// <summary>
        /// Gets the current value of the Billing City field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingCity()
        {
            return driver.FindElement(By.Id("wpsc_checkout_form_5")).GetAttribute("value");
        }

        /// <summary>
        /// Gets the current value of the Billing Country field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingCountry()
        {
            IWebElement countryList = driver.FindElement(By.Id("wpsc_checkout_form_7"));
            SelectElement countryDropdown = new SelectElement(countryList);
            return countryDropdown.SelectedOption.Text;
        }

        /// <summary>
        /// Gets the current value of the Billing Postal Code field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingPostalCode()
        {
            return driver.FindElement(By.Id("wpsc_checkout_form_8")).GetAttribute("value");
        }

        /// <summary>
        /// Gets the current value of the Billing Phone field
        /// </summary>
        /// <returns>The value of the field</returns>
        public string BillingPhone()
        {
            return driver.FindElement(By.Id("wpsc_checkout_form_18")).GetAttribute("value");
        }

        /// <summary>
        /// Finds and clicks on the Save Profile button
        /// </summary>
        /// <returns>The current page object</returns>
        public AccountPage SaveProfile()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        /// <summary>
        /// Logs out of the site
        /// </summary>
        /// <returns>The LoginPage page object</returns>
        public LoginPage Logout()
        {
            //driver.FindElement(By.LinkText("Log out")).Click();

            driver.FindElement(By.Id("sidebar-right")).FindElement(By.LinkText("Log out")).Click();
            return new LoginPage(driver);
        }
        #endregion
    }
}
