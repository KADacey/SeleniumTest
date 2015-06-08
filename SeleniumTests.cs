using System;
using System.Configuration;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SeleniumTest
{
    [TestClass]
    public class SeleniumTests : Configuration
    {
        private TestContext context;
        protected IWebDriver driver;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test
        /// </summary>
        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        [TestInitialize]
        public void Setup()
        {
            driver = BrowserDriver;
        }

        /// <summary>
        /// This test searches for a particular model of phone, adds it to the shopping cart,
        /// proceeds the checkout flow and verifies the total price before completing the purchase.
        /// This is done via a user who is not logged into the site.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", ".\\PurchasePhoneTest.xml", "Row", DataAccessMethod.Sequential)]
        public void PurchasePhoneTest_UserNotLoggedIn()
        {
            //retrieve the test data
            string productDisplayName = Convert.ToString(context.DataRow["productDisplayName"]);
            string productTextName = Convert.ToString(context.DataRow["productTextName"]);
            string countryName = Convert.ToString(context.DataRow["countryName"]);
            string emailAddress = Convert.ToString(context.DataRow["emailAddress"]);
            string firstName = Convert.ToString(context.DataRow["firstName"]);
            string lastName = Convert.ToString(context.DataRow["lastName"]);
            string address = Convert.ToString(context.DataRow["address"]);
            string cityName = Convert.ToString(context.DataRow["cityName"]);
            string postalCode = Convert.ToString(context.DataRow["postalCode"]);
            string phoneNumber = Convert.ToString(context.DataRow["phoneNumber"]);
            decimal expectedPrice = Convert.ToDecimal(context.DataRow["expectedPrice"]);

            //navigate to the given URL and maximize the browser
            driver.Navigate().GoToUrl(TestURL);
            driver.Manage().Window.Maximize();

            //instantiate the HomePage
            HomePage Home = new HomePage(driver);

            //logout in order to proceed as a 'new' user
            Home.Logout();

            //search for the product
            SearchResultsPage Search = Home.TypeProductSearch(productTextName);

            //navigate to the product page
            ProductPage Product = Search.NavigateToProductPage(productDisplayName, productTextName);

            //add the product to the cart
            Product.AddToCart();

            //navigate to the checkout page
            CheckoutPage Checkout = Product.GoToCheckout();

            //continue the checkout process by filling in billing and shipping info
            Checkout.ContinueCheckout();
            Checkout.SelectCountryAndCalculateShipping(countryName);
            Checkout.TypeEmailAddress(emailAddress);
            Checkout.TypeBillingFirstName(firstName);
            Checkout.TypeBillingLastName(lastName);
            Checkout.TypeBillingAddress(address);
            Checkout.TypeBillingCity(cityName);
            Checkout.TypeBillingCountry(countryName);
            Checkout.TypeBillingPostalCode(postalCode);
            Checkout.TypeBillingPhone(phoneNumber);
            Checkout.SelectBillingAsShipping();

            //validate the price is as expected
            decimal totalPrice = Checkout.TotalPrice();
            Assert.AreEqual(totalPrice, expectedPrice);

            //complete the purchase
            TransactionResultPage PurchaseResult = Checkout.MakePurchase();
        }

        /// <summary>
        /// This test case will login, change the user's address, logout, login a second time,
        /// and ensure the updated address has been saved between the logins.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", ".\\UpdateAccountTest.xml", "Row", DataAccessMethod.Sequential)]
        public void UpdatedAccountAddressIsSaved()
        {
            //retrieve the test data
            string username = Convert.ToString(context.DataRow["username"]);
            string password = Convert.ToString(context.DataRow["password"]);
            string expectedAddress = Convert.ToString(context.DataRow["expectedAddress"]);

            //note: these values should come from an external source to make the test case data-driven
            //      I'm hardcoding the values here just for expediency
            //string expectedAddress = "789 Main Street";
            //string givenUserName = "KADacey";
            //string givenPassword = "APassword";

            //navigate to the given URL and maximize the browser
            driver.Navigate().GoToUrl(TestURL);
            driver.Manage().Window.Maximize();

            //instantiate the HomePage
            HomePage Home = new HomePage(driver);

            //logout if a user is already logged in
            //Home.Logout();

            //navigate the the account page
            AccountPage Account = Home.NavigateToAccountPage();

            //enter username and password then login
            Account.TypeUsername(username);
            Account.TypePassword(password);
            Account.ClickLoginButton();

            //navigate to the details page
            Account.NavigateToYourDetails();

            //get the current values
            //ideally these would be saved and restored once the test is complete
            //currently this only happens if the test runs to completion
            //an unexpected error will leave the data in an undetermined state
            string savedFirstName = Account.BillingFirstName();
            string savedLastName = Account.BillingLastName();
            string savedAddress = Account.BillingAddress();
            string savedCity = Account.BillingCity();
            string savedCountry = Account.BillingCountry();
            string savedPostalCode = Account.BillingPostalCode();
            string savedPhone = Account.BillingPhone();

            //check to ensure the current address isn't already equal to our expected address
            if (String.Equals(expectedAddress, savedAddress))
            {
                expectedAddress = "";   //remove the address if the expected is already there
            }

            //update the address and save
            Account.TypeBillingAddress(expectedAddress);
            Account.SaveProfile();

            //logout and then log back in
            LoginPage Login = Account.Logout();
            Login.TypeUsername(username);
            Login.TypePassword(password);
            ProfilePage Profile = Login.ClickLoginButton();

            //we end up on the profile page so navigate home and then back to the account page
            Home = Profile.NavigateToHomePage();
            Account = Home.NavigateToAccountPage();

            //navigate back to the details page
            Account.NavigateToYourDetails();

            //retrieve the current address value and compare to expected
            string actualAddress = Account.BillingAddress();
            Assert.AreEqual(actualAddress, expectedAddress, true);  //true indicates we're ignoring case sensitivity

            //be a good citizen and restore everything back to the original values
            Account.TypeBillingFirstName(savedFirstName);
            Account.TypeBillingLastName(savedLastName);
            Account.TypeBillingAddress(savedAddress);
            Account.TypeBillingCity(savedCity);
            Account.TypeBillingCountry(savedCountry);
            Account.TypeBillingPostalCode(savedPostalCode);
            Account.TypeBillingPhone(savedPhone);
            Account.SaveProfile();
        }

        /// <summary>
        /// This test will search for and add an item to the cart then remove it,
        /// validating that the empty cart message is shown as expected.
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", ".\\EmptyCartTest.xml", "Row", DataAccessMethod.Sequential)]
        public void ValidateEmptyCartMessageAfterItemRemoved()
        {
            //retrieve the test data
            string productDisplayName = Convert.ToString(context.DataRow["productDisplayName"]);
            string productTextName = Convert.ToString(context.DataRow["productTextName"]);

            //navigate to the given URL and maximize the browser
            driver.Navigate().GoToUrl(TestURL);
            driver.Manage().Window.Maximize();

            //instantiate the HomePage
            HomePage Home = new HomePage(driver);

            //search for the product
            SearchResultsPage Search = Home.TypeProductSearch(productTextName);

            //navigate to the product page
            ProductPage Product = Search.NavigateToProductPage(productDisplayName, productTextName);

            //add the product to the cart
            Product.AddToCart();

            //navigate to the checkout page
            CheckoutPage Checkout = Product.GoToCheckout();

            //remove the item from the cart
            Checkout.RemoveItem();

            Assert.IsTrue(Checkout.IsEmptyCartMessageShown("Oops, there is nothing in your cart."));
        }

        [TestCleanup]
        public void TearDown()
        {
            try
            {
                driver.Close();
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error during tear down: " + e.Message);
            }
        }

    }
}
