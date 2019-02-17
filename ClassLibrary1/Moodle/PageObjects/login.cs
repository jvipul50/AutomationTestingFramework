using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using Moodle;

namespace Moodle
{
    public class login
    {
        #region Private Variables

        private IWebDriver Login;
        private ExtentTest test;

        #endregion

        #region Public Methods

        public login(IWebDriver driver, ExtentTest test1)
        {
            this.Login = driver;
            test = test1;
        }

        public IWebDriver driver    
        {
            get { return this.Login; }
            set { this.Login = value; }
        }

        /// <summary>
        /// This method navigates to login page.
        /// </summary>
        /// <returns>login</returns>
        public login navigateToLoginPage()
        {
            driver.Navigate().GoToUrl("https://moodle.cestarcollege.com/moodle/login/index.php");
            driver.waitForElement("id", "username");
            Results.WriteStatus(test, "Pass", "Navigated to Login Page sucessfully.");

            return new login(driver, test);
        }

        /// <summary>
        /// This method verify login page
        /// </summary>
        /// <returns>login</returns>
        public login verifyLoginPage()
        {
            driver.waitForElement("xpath", "//*[@id='page-header']/div[1]/div/h1");
            Assert.IsTrue(driver.isElementPresent("xpath", "//*[@id='page-header']/div[1]/div/h1"), "Lambton heading not found.");
            Results.WriteStatus(test, "Pass", "Verified heading field successfully.");

            driver.waitForElement("xpath", "//*[@class='loginpanel']/h2");
            Assert.IsTrue(driver.isElementPresent("xpath", "//*[@class='loginpanel']/h2"), "Login label not found.");
            Results.WriteStatus(test, "Pass", "Verified Login Label successfully.");

            driver.waitForElement("xpath", "//*[@class='loginform']/div[1]/label");
            Assert.IsTrue(driver.isElementPresent("xpath", "//*[@class='loginform']/div[1]/label"), "Username label not found.");
            Results.WriteStatus(test, "Pass", "Verified Username Label successfully.");

            driver.waitForElement("id", "username");
            Assert.IsTrue(driver.isElementPresent("id", "username"), "Username field not found.");
            Results.WriteStatus(test, "Pass", "Verified Username input field successfully.");

            driver.waitForElement("xpath", "//*[@class='loginform']/div[4]/label");
            Assert.IsTrue(driver.isElementPresent("xpath", "//*[@class='loginform']/div[4]/label"), "Password label not found.");
            Results.WriteStatus(test, "Pass", "Verified Password Label successfully.");

            driver.waitForElement("id", "password");
            Assert.IsTrue(driver.isElementPresent("id", "password"), "Password field not found.");
            Results.WriteStatus(test, "Pass", "Verified Passwordinput field successfully.");

            driver.waitForElement("id", "loginbtn");
            Assert.IsTrue(driver.isElementPresent("id", "loginbtn"), "Log in button not found.");
            Results.WriteStatus(test, "Pass", "Verified Login button successfully.");
            
            return new login(driver, test);
        }

        /// <summary>
        /// This method is used to enter Username and Password on website.
        /// </summary>
        /// <returns></returns>
        public login enterUserNameAndPassword(string username="715908", string password="Lupiv@321")
        {
            driver.waitForElement("id", "username");
            Assert.IsTrue(driver.isElementPresent("id", "username"), "Username field not found.");
            driver.type("id", "username", username);
            Results.WriteStatus(test, "Pass", "Successfully entered " + username + " as Username.");

            driver.waitForElement("id", "password");
            Assert.IsTrue(driver.isElementPresent("id", "password"), "Password field not found.");
            driver.type("id", "password", password);
            Results.WriteStatus(test, "Pass", "Successfully entered password.");
            
            return new login(driver, test);
        }

        /// <summary>
        /// This method clicks on login button
        /// </summary>
        /// <returns></returns>
        public login clickOnLoginButton()
        {

            Assert.IsTrue(driver.isElementPresent("id", "loginbtn"), "Login button not found.");
            driver.click("id", "loginbtn");
            Results.WriteStatus(test, "Pass", "Clicked Login button successfully.");

            return new login(driver, test);
        }
        #endregion
    }
}
