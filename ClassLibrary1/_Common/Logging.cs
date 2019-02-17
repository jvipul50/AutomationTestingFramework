using System;
using System.Collections.Generic;
using System.Linq;using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using System.IO;
using Moodle;
using AventStack.ExtentReports;
using System.Reflection;
using System.Diagnostics;
using System.Xml;

namespace Moodle
{
    public static class Logging
    {
        
        public static void LogStop(IWebDriver driver, ExtentTest test, Exception e, MethodBase methodBase, string scenarioTC)
        {
            bool avail = false;
            string GetMethoName = "";
            string summary = "";

            try
            {
                StackTrace trace = new StackTrace(e);

                string getlatest = "";
                

                foreach (StackFrame frame in trace.GetFrames())
                {
                    if (frame.GetMethod() == methodBase)
                    {
                        break;
                    }
                    getlatest = frame.GetMethod().ToString();
                    GetMethoName = frame.GetMethod().Name;
                }
                //string Methodname = getlatest.Replace(" ", ".");

                string xmlFilePath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
                xmlFilePath = xmlFilePath.Replace(".DLL", ".xml");
                xmlFilePath = xmlFilePath.Replace("%20", " ");

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                var membersNode = xmlDoc.DocumentElement["members"];
                
                if (membersNode != null)
                {
                    foreach (XmlNode memberNode in membersNode.ChildNodes)
                    {
                        try
                        {
                            if (memberNode.Attributes["name"].Value.Contains(GetMethoName))
                            {
                                avail = true;
                                summary = memberNode["summary"].InnerText;
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
                Common.ErrorMethod = GetMethoName;
            }

            Common.ErrorMethodName = GetMethoName;
            if (avail == false)
            {
                Common.ErrorMethod = GetMethoName;
            }
            else
            {
                Common.ErrorMethod = summary;
            }

            if (e.Message == "The 'Microsoft.Jet.OLEDB.4.0' provider is not registered on the local machine." || e.Message == "The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine.")
            {
                string screenshotName = scenarioTC + ".jpg";
                string screenshotPath = Path.GetFullPath(Common.currentReportLocation) + "\\" + screenshotName;

                Results.WriteStatus(test, "Fail", "<br><b>Your Microsoft office version does not have full support please download microsoft solution from below link...<br>http://www.microsoft.com/en-us/download/details.aspx?id=13255");
            }
            else
            {
                string screenshotName = scenarioTC + ".png";
                string screenshotPath = Path.GetFullPath(Common.currentReportLocation) + "\\" + screenshotName;
                driver._takeScreenshot(screenshotPath);

                Console.WriteLine("screenshotName Logging : " + screenshotName);
                Console.WriteLine("screenshotPath Logging : " + screenshotPath);

                Results.WriteStatus(test, "Fail", "<br><b> ERROR Step : <font color='red'> " + Common.ErrorMethod + "</font></b><br>" + "<br><b>ERROR Message:</b><br>" + e.Message + "<br><br><b>StackTrace:</b><br> " + e.StackTrace + "<br><br><br>Screenshot of the Page where the execution Stopped.<a>", screenshotPath);
            }

            driver.Quit();
        }       
    }
}
