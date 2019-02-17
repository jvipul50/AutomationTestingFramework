using System;
using System.Collections.Generic;
using System.Linq;using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AventStack.ExtentReports;
using System.Configuration;
using Moodle;
using System.Reflection;
using AventStack.ExtentReports.Reporter;


namespace Moodle
{
    [SetUpFixture]
    public class SetUpFixture : Base
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            extent = ExtentManager.Instance;

            string text = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            string DatasheetPath = "ClassLibrary1/bin/Debug/extent-xml.xml";

            string file = text.Replace(DatasheetPath, "");
            string filePath = file.Replace("ClassLibrary1.DLL", "extent-xml.xml");
            filePath = filePath.Replace("%20", " ");
            Console.WriteLine("filePath : " + filePath);

            //ExtentManager.extentxReporter = new ExtentXReporter("mongodb://108.26.234.206:27000");            

            //ExtentManager.extentxReporter.LoadConfig(@"" + filePath + "");

            ExtentManager.htmlReporter.LoadConfig(@"" + filePath + "");

            extent.AttachReporter(ExtentManager.htmlReporter);//, ExtentManager.extentxReporter);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}
