using System;
using System.Collections.Generic;
using System.Linq;using System.Reflection;
using System.Text;
using OpenQA.Selenium;

namespace Moodle
{
    public static class Common
    {
        #region Private Methods

        private static TimeSpan _defaultTimeSpan = new TimeSpan(0, 0, 30);

        #endregion

        #region Public Methods

        public static string WebBrowser { get; set; }
        public static TimeSpan DriverTimeout { get { return _defaultTimeSpan; } set { _defaultTimeSpan = value; } }
        public static IWebDriver CurrentDriver { get; set; }
        public static int OSbit { get; set; }
        public static string currentReportLocation { get; set; }
        public static string currentTestSuite { get; set; }
        public static string currentTestScenario { get; set; }
        public static string scenarioNumberForSS { get; set; }

        public static string TestSite { get; set; }

        public static string UserID { get; set; }
        public static string Passwd { get; set; }

        public static string ErrorMethod { get; set; }
        public static string ErrorMethodName { get; set; }

        public static string OsName { get; set; }
        public static string OsVersion { get; set; }
        public static string BrowserVersion { get; set; }

        public static string Platform { get; set; }


        #endregion
    }

}
