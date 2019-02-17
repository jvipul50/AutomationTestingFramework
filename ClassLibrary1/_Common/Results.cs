using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using NUnit;
using System.Configuration;
using System.Web;
using System.Diagnostics;
using Moodle;
using AventStack.ExtentReports;

namespace Moodle
{
    public static class Results
    {
        
        #region Public Methods

        public static void WriteTestSuiteHeading(string testsuite, bool isPreRequisite = false)
        {
            Common.currentTestSuite = testsuite;
            Common.currentReportLocation = ExtentManager.ResultsDir;
        }

        public static void WriteScenarioHeading(string scenarioname, bool isPreRequisite = false)
        {
            Common.currentTestScenario = scenarioname;
            Common.scenarioNumberForSS = scenarioname.Split('_')[0].Trim();
        }

        public static void WriteStatus(ExtentTest test, string PassOrFail, string Message = "", string ssPath = "")
        {

            if (PassOrFail.ToLower().Equals("pass"))
            {
                test.Log(Status.Pass, Message);//ExtentReport
            }
            else if (PassOrFail.ToLower().Equals("fail"))
            {
                test.Log(Status.Fail, Message);//ExtentReport
                test.AddScreenCaptureFromPath(ssPath);
            }
            if (PassOrFail.ToLower().Trim().Contains("warn"))
            {
                test.Log(Status.Warning, Message);//ExtentReport
            }

        }

        #endregion

    }
}

