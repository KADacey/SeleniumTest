using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class SearchResultsPage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Search Results"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// SearchResultsPage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1) 
            {
                throw new NotFoundException("This is not the Search Results page:" + driver.Title);
            }
        }

        /// <summary>
        /// Navigate to the product page by click on the given product link
        /// </summary>
        /// <param name="productDisplayName">The product display name</param>
        /// <param name="productTextName">The product text name</param>
        /// <returns></returns>
        public ProductPage NavigateToProductPage(string productDisplayName, string productTextName)
        {
            driver.FindElement(By.LinkText(productDisplayName)).Click();
            return new ProductPage(driver, productDisplayName, productTextName);
        }
    }
}
