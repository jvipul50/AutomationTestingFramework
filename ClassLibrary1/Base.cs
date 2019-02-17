using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moodle
{
    public class Base
    {
        #region Private Variables

        private FirefoxProfile _ffp;
        private InternetExplorerOptions _ie;
        private ChromeOptions _chrome;
        private DesiredCapabilities _cap;
        private IWebDriver _driver;
        private FirefoxBinary _bin;

        protected ExtentReports extent;
        protected ExtentTest test;

        #endregion

        #region Public Methods

        [OneTimeSetUp]
        public void FixtureInit()
        {
            extent = ExtentManager.Instance;

            string environment = ConfigurationSettings.AppSettings["TestSite"];
            Common.TestSite = environment;          
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Console.WriteLine("status");
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }            
        }

        public static IEnumerable<String> BrowserToRun()
        {
            //String[] browsers = { "firefox", "chrome", "ie" };
            IList<String> browsers = ConfigurationSettings.AppSettings["Browser"].Split(',').ToList();

            foreach (String b in browsers.ToArray())
            {
                yield return b;
            }
        }

        public void starttest(string str, params string[] Category)
        {
            test = extent.CreateTest(str).AssignCategory(Category);
            Results.WriteScenarioHeading(str.Remove(0, str.IndexOf("T")));
        }

        public IWebDriver StartBrowser(String Bname)
        {
            string driverDir = System.Configuration.ConfigurationSettings.AppSettings["DriverDir"];

            string driverPath = "";
            
            switch (Bname.ToLower())
            {
                case "firefox":
                    driverPath = driverDir + "\\Firefox";
                    FirefoxOptions _options = new FirefoxOptions();
                    _driver = new FirefoxDriver(driverPath, _options, TimeSpan.FromSeconds(120));
                    break;
                case "chrome":
                    driverPath = driverDir + "\\Chrome";
                    _chrome = new ChromeOptions();
                    _chrome.AddArguments("test-type");
                    _chrome.AddArguments("chrome.switches", "--disable-extensions");
                    _chrome.AddArguments("disable-infobars");
                    _chrome.AddUserProfilePreference("credentials_enable_service", false);
                    _driver = new ChromeDriver(driverPath, _chrome, TimeSpan.FromSeconds(120));
                    break;
                    
            }
            
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Window.Maximize();
            
            return _driver;
        }

        #endregion
    }
}
