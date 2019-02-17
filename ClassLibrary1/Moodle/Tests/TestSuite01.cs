using Moodle;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace Moodle
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TestSuite01 : Base
    {
        #region Private Variables

        private IWebDriver driver;
        login loginPage;

        #endregion

        #region Public Fixture Methods

        public IWebDriver testFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite01).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite01).Name);

            loginPage = new login(driver, test);

            return driver;
        }

        [TearDown]
        public void testFixtureTearDown()
        {
            //extent.EndTest();
            driver.Quit();
        }

        #endregion

        #region Test Methods

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC01_LoginAndVerify(String Bname)
        {
            testFixtureSetUp(Bname, "TC01_Navigate to Login Page and verify.");
            try
            {
                loginPage = loginPage.navigateToLoginPage();
                loginPage.verifyLoginPage();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite01_TC01");
                throw;
            }
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC02_EnterValidCredentialAndLogin(String Bname)
        {
            testFixtureSetUp(Bname, "TC02_Enter valid credential and login.");
            try
            {
                loginPage = loginPage.navigateToLoginPage();
                loginPage.verifyLoginPage();
                loginPage.enterUserNameAndPassword();
                loginPage.clickOnLoginButton();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite01_TC02");
                throw;
            }
        }

        #endregion


    }
}
