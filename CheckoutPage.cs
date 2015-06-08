using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class CheckoutPage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Checkout"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// CheckoutPage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1) 
            {
                throw new NotFoundException("This is not the Checkout page: " + driver.Title);
            }
        }

        /// <summary>
        /// Continue the checkout process by clicking on the continue button
        /// </summary>
        /// <returns>Current page object</returns>
        public CheckoutPage ContinueCheckout()
        {
            driver.FindElement(By.ClassName("step2")).Click();
            return this;
        }

        /// <summary>
        /// Select the given country and click on Calculate button
        /// </summary>
        /// <param name="country">The country where the product will be shipped</param>
        /// <returns>Current page object</returns>
        public CheckoutPage SelectCountryAndCalculateShipping(string country)
        {
            var countryDropdown = driver.FindElement(By.Id("current_country"));
            var selectElement = new SelectElement(countryDropdown);

            selectElement.SelectByText(country);
            driver.FindElement(By.Name("wpsc_submit_zipcode")).Click();
            return this;
        }

        /// <summary>
        /// Enter the given string into the Email address field
        /// </summary>
        /// <param name="email">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeEmailAddress(string email)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_9"));
            field.Clear();
            field.SendKeys(email);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing First Name field
        /// </summary>
        /// <param name="firstName">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingFirstName(string firstName)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_2"));
            field.Clear();
            field.SendKeys(firstName);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing Last Name field
        /// </summary>
        /// <param name="lastName">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingLastName(string lastName)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_3"));
            field.Clear();
            field.SendKeys(lastName);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing Address field
        /// </summary>
        /// <param name="address">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingAddress(string address)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_4"));
            field.Clear();
            field.SendKeys(address);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing City field
        /// </summary>
        /// <param name="city">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingCity(string city)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_5"));
            field.Clear();
            field.SendKeys(city);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing State field
        /// </summary>
        /// <param name="state">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingState(string state)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_6"));
            field.Clear();
            field.SendKeys(state);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing Country field
        /// </summary>
        /// <param name="country">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingCountry(string country)
        {
            var countryDropdown = driver.FindElement(By.Id("wpsc_checkout_form_7"));
            var selectElement = new SelectElement(countryDropdown);

            selectElement.SelectByText(country);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing Postal Code field
        /// </summary>
        /// <param name="postalCode">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingPostalCode(string postalCode)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_8"));
            field.Clear();
            field.SendKeys(postalCode);
            return this;
        }

        /// <summary>
        /// Enter the given string into the Billing Phone number field
        /// </summary>
        /// <param name="phone">The string to enter</param>
        /// <returns>Current page object</returns>
        public CheckoutPage TypeBillingPhone(string phone)
        {
            IWebElement field = driver.FindElement(By.Id("wpsc_checkout_form_18"));
            field.Clear();
            field.SendKeys(phone);
            return this;
        }

        /// <summary>
        /// Check the Same as Billing Address field
        /// This should determine if the field is already checked but doesn't currently
        /// </summary>
        /// <returns>Current page object</returns>
        public CheckoutPage SelectBillingAsShipping()
        {
            IWebElement checkbox = driver.FindElement(By.Id("shippingSameBilling"));
            if (!checkbox.Selected)
            {
                checkbox.Click();
            }

            return this;
        }

        /// <summary>
        /// Clicks on the Purchase button
        /// </summary>
        /// <returns>The TransactionResult page object</returns>
        public TransactionResultPage MakePurchase()
        {
            driver.FindElement(By.ClassName("make_purchase")).Click();
            return new TransactionResultPage(driver);
        }

        
        /// <summary>
        /// Removes the item from the cart
        /// Note: this should be made more flexible with a parameter indicating the product to remove
        /// Currently assumes only 1 product in the cart
        /// </summary>
        /// <returns>Current page object</returns>
        public CheckoutPage RemoveItem()
        {
            IWebElement form = driver.FindElement(By.ClassName("wpsc_product_remove"));
            form.FindElement(By.ClassName("adjustform")).Submit();
            return this;
        }

        /// <summary>
        /// Checks if the given string is shown
        /// </summary>
        /// <param name="expectedMsg">The empty cart string</param>
        /// <returns>True if shown, false otherwise</returns>
        public bool IsEmptyCartMessageShown(string expectedMsg)
        {
            string actualValue = driver.FindElement(By.ClassName("entry-content")).Text;
            return (String.Equals(expectedMsg, actualValue, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the total shipping price
        /// </summary>
        /// <returns></returns>
        public decimal TotalShipping()
        {
            decimal totalShipping = 0;
            string totalShippingStr;

            By totalShippingLocator = By.ClassName("total_price total_shipping");
            totalShippingStr = driver.FindElement(totalShippingLocator).FindElement(By.ClassName("price_display")).ToString();
            if (Decimal.TryParse(totalShippingStr, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out totalShipping))
            {
                return totalShipping;
            }
            else
            {
                throw new FormatException("Total price: " + totalShippingStr);
            }
        }

        /// <summary>
        /// Gets the total item price
        /// </summary>
        /// <returns></returns>
        public decimal TotalItem()
        {
            decimal totalItem = 0;
            string totalItemStr;

            By totalItemLocator = By.ClassName("total_price total_item");
            totalItemStr = driver.FindElement(totalItemLocator).FindElement(By.ClassName("price_display")).ToString();
            if (Decimal.TryParse(totalItemStr, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out totalItem))
            {
                return totalItem;
            }
            else
            {
                throw new FormatException("Total price: " + totalItemStr);
            }
        }

        /// <summary>
        /// Gets the total tax amount
        /// </summary>
        /// <returns></returns>
        public decimal TotalTax()
        {
            decimal totalTax = 0;
            string totalTaxStr;

            By totalTaxLocator = By.ClassName("total_price total_tax");
            totalTaxStr = driver.FindElement(totalTaxLocator).FindElement(By.ClassName("price_display")).ToString();
            if (Decimal.TryParse(totalTaxStr, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out totalTax))
            {
                return totalTax;
            }
            else
            {
                throw new FormatException("Total price: " + totalTaxStr);
            }
        }

        /// <summary>
        /// Gets the total price amount
        /// </summary>
        /// <returns></returns>
        public decimal TotalPrice()
        {
            decimal totalPrice = 0;
            string totalPriceStr;

            totalPriceStr = driver.FindElement(By.Id("checkout_total")).Text;
            if (Decimal.TryParse(totalPriceStr, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out totalPrice))
            {
                return totalPrice;
            }
            else
            {
                throw new FormatException("Total price: " + totalPriceStr);
            }
        }
    }
}
