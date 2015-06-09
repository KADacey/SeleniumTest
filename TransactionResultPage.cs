using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    class TransactionResultPage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Transaction Results"; //ideally this should be pulled from a configuration or data file

        /// <summary>
        /// TransactionResultPage constructor
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public TransactionResultPage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return !(d.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1); });

            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                throw new NotFoundException("This is not the Transaction Result page: " + driver.Title);
            }
        }

    }
}
