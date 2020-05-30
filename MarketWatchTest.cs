using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWatch
{
    public class MarketWatchTest
    {
        private IWebDriver webDriver;
        private string url;
        private IDictionary<String, String> dictionary;
        private ExtentReports extentReports;
        private ExtentTest extentTest;

        [OneTimeSetUp]
        public void MarketWatchTestReportSetup()
        {
            //To obtain the current solution path/project path

            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

            string actualPath = path.Substring(0, path.LastIndexOf("bin"));

            string projectPath = new Uri(actualPath).LocalPath;

            //Append the html report file to current project path

            string reportPath = projectPath + "Reports\\MarketWatchRunReport.html";

            Debug.WriteLine(reportPath);

            //Boolean value for replacing exisisting report

            extentReports = new ExtentReports(reportPath, true);

            //Add QA system info to html report
            extentReports.AddSystemInfo("NOVAC", "localhost")

                .AddSystemInfo("Environment", "Windows")

                .AddSystemInfo("Username", "Parameswari");
            //Adding config.xml file
            extentReports.LoadConfig(projectPath + "Extent-Config.xml");

        }


       [SetUp]
        public void Init()
        {
            dictionary = ResourceHelper.GetAttributes();
            webDriver = new ChromeDriver(dictionary["driver"].ToString());
            url = dictionary["url"].ToString();

        }

        [Test]
        public void MarketWatchDynamicWebTableTest()
        {
            webDriver.Url = url;
        }

        [TearDown]
        public void MarketWatchTestClear()
        {
            extentReports.EndTest(extentTest);
            webDriver.Close();
        }


        [OneTimeTearDown]
        public void EndReport()

        {
             //End Report

            extentReports.Flush();
            extentReports.Close();

        }
    }
}
