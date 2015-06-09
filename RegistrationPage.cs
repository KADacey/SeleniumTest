using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTest
{
    class RegistrationPage
    {
        private IWebDriver driver;
        private static string titleSubstring = "Registration Form"; //ideally this should be pulled from a configuration or data file

        public RegistrationPage(IWebDriver driver)
        {
            this.driver = driver;

            // Check that we're on the right page, ignoring case
            if (driver.Title.IndexOf(titleSubstring, StringComparison.CurrentCultureIgnoreCase) == -1)
            {
                throw new IllegalLocatorException("This is not the Registration page");
            }
        }

        public RegistrationPage TypeUsername(string userName)
        {
            driver.FindElement(By.Id("user_login")).SendKeys(userName);
            return this;
        }

        public RegistrationPage TypePassword(string email)
        {
            driver.FindElement(By.Id("user_email")).SendKeys(email);
            return this;
        }

        public ProfilePage ClickLoginButton()
        {
            driver.FindElement(By.Id("login")).Click();
            return new ProfilePage(driver);
        }

        //assumes valid possible numbers in the equation are 0-20 inclusive
        //ideally there would be a test hook of some kind so this clunky code wouldn't be needed
        public int ComputeMathCaptcha()
        {
            int captchaResult = 0;
            string captchaEquation = driver.FindElement(By.ClassName("aiowps-captcha-equation")).ToString();
            char[] separator = {' '};
            string[] mathElements = captchaEquation.Split(separator, 3);
            int firstNum = ConvertStringToNum(mathElements[0]);
            int secondNum = ConvertStringToNum(mathElements[2]);

            switch (mathElements[1])
            {
                case "+":
                    captchaResult = firstNum + secondNum;
                    break;
                case "-":
                    captchaResult = firstNum - secondNum;
                    break;
                case "*":
                    captchaResult = firstNum * secondNum;
                    break;
                default:
                    captchaResult = -1;
                    break;
            }

            return captchaResult;
        }

        private int ConvertStringToNum(string num)
        {
            int numberValue;
            string[] wordNums = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                                "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty"};

            try
            {
                numberValue = Convert.ToInt32(num);
            }
            catch (FormatException)
            {
                numberValue = Array.IndexOf(wordNums, num);
            }

            return numberValue;
        }
    }
}
