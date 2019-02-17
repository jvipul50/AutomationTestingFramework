using System;
using System.Collections.Generic;
using System.Linq;using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Reflection;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace Moodle
{
    internal class ExtentManager
    {

        public static String reporttime = System.DateTime.Now.ToString("yyyy-dd-MM.hh.mm.ss");
        public static String HTMLResultsLocation = ConfigurationSettings.AppSettings["HTMLResultsLocation"];
        public static string folderName = "_MoodleAutomation_" + reporttime;
        public static string ResultsDir = Path.Combine(Path.GetFullPath(HTMLResultsLocation), folderName);

        public static DirectoryInfo x = System.IO.Directory.CreateDirectory(ResultsDir);

        public static String FileName1 = "_MoodleAutomation_" + reporttime;//ExtentReport
        public static String resultsFileName1 = Path.Combine(ResultsDir, FileName1 + ".html");
        
        public static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(resultsFileName1);

        public static ExtentXReporter extentxReporter;// = new ExtentXReporter("mongodb://108.26.234.206:27056");
        
        public static ExtentReports _instance = new ExtentReports();

        static ExtentManager() { }

        private ExtentManager() { }

        public static ExtentReports Instance
        {
            get
            {
                //string text = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
                //string DatasheetPath = "ClassLibrary1/bin/Debug/extent-xml.xml";

                //string file = text.Replace(DatasheetPath, "");
                //string filePath = file.Replace("ClassLibrary1.DLL", "extent-xml.xml");
                //filePath = filePath.Replace("%20", " ");

                //extentxReporter.LoadConfig(@"" + filePath + "");

                return _instance;
            }
        }
    }
}

