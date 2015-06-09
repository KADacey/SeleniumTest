using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class ProductPage
    {
        private IWebDriver driver;

        /// <summary>
        /// ProductPage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="productDisplayName">The display name of the product</param>
        /// <param name="productTextName">The text name of the product, which can differ from the display name</param>
        public ProductPage(IWebDriver driver, string productDisplayName, string productTextName)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(productTextName, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(productTextName, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                throw new NotFoundException("This is not the Product page for:" + productTextName);
            }
        }

        /// <summary>
        /// Click the Add To Cart button
        /// </summary>
        /// <returns>Current page object</returns>
        public ProductPage AddToCart()
        {
            driver.FindElement(By.Name("Buy")).Click();
            return this;
        }

        /// <summary>
        /// Click the Go to Checkout button on the "Just added" notification popup
        /// </summary>
        /// <returns>THe resulting Checkout page object</returns>
        public CheckoutPage GoToCheckout()
        {
            By notificationPopup = By.Id("fancy_notification_content");
            
            driver.FindElement(notificationPopup).FindElement(By.LinkText("Go to Checkout")).Click();
            return new CheckoutPage(driver);
        }
    }
}
