using LumenWorks.Framework.IO.Csv;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryTours
{
    [TestFixture]
    public class MercuryToursTest
    {
        private IWebDriver webDriver;
        private String test_url;
        private Hashtable hashtable;
        [SetUp]
        public void InitializeWebDriver()
        {
            hashtable = ResourceHelper.GetKeyandValues();
            webDriver = new ChromeDriver(hashtable["driver"].ToString());
            test_url = hashtable["url"].ToString();
        }
        [Test]
        public void OpenURLTest()
        {
            webDriver.Url = test_url;
        }
        //[Ignore("Special Character issue")]
        [Test]
        public void GetTitleTest()
        {
            webDriver.Url = test_url;
            String actualTitle = webDriver.Title;
            Assert.AreEqual(ResourceHelper.GetKeyandValues()["expectedTitle"].ToString()
                , actualTitle);
        }
        [Test,TestCaseSource("ReadDatafromCSV")]
        public void LoginTest(String userName,String password)
        {
            webDriver.Url = test_url;
            webDriver.FindElement(By.Name("userName")).SendKeys(userName);
            webDriver.FindElement(By.Name("password")).SendKeys(password);
            webDriver.FindElement(By.Name("login")).Click();

        }

        [Test]
        public void FlightFinderTest()
        {
            webDriver.Url = test_url;
            webDriver.FindElement(By.Name("userName")).SendKeys("eswaribala");
            webDriver.FindElement(By.Name("password")).SendKeys("vigneshbala");
            webDriver.FindElement(By.Name("login")).Click();
            //making it to wait for 2000 ms
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);
            //radio button
           IList<IWebElement> elements= webDriver.FindElements(By.Name("tripType"));
           //initial value
            String text = "";
            foreach(IWebElement element in elements)
            {
                text = element.GetAttribute("value");
                if (text.Equals("oneway"))
                    element.Click();
            }
            //drop down list
            IWebElement passengerElement = webDriver.FindElement(By.Name("passCount"));
            SelectElement selectElement = new SelectElement(passengerElement);
            selectElement.SelectByValue("2");
            //making it to wait for 2000 ms
            //webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);
            IWebElement fromPortElement = webDriver.FindElement(By.Name("fromPort"));
            SelectElement selectFromPortElement = new SelectElement(fromPortElement);
            selectFromPortElement.SelectByValue("London");
            IWebElement fromMonthElement = webDriver.FindElement(By.Name("fromMonth"));
            SelectElement selectFromMonthElement = new SelectElement(fromMonthElement);
            selectFromMonthElement.SelectByValue("7");

            IWebElement fromDayElement = webDriver.FindElement(By.Name("fromDay"));
            SelectElement selectFromDayElement = new SelectElement(fromDayElement);
            selectFromDayElement.SelectByValue("14");

            IWebElement toPortElement = webDriver.FindElement(By.Name("toPort"));
            SelectElement selectToPortElement = new SelectElement(toPortElement);
            selectToPortElement.SelectByValue("Paris");
            IWebElement toMonthElement = webDriver.FindElement(By.Name("toMonth"));
            SelectElement selectToMonthElement = new SelectElement(toMonthElement);
            selectToMonthElement.SelectByValue("7");

            IWebElement toDayElement = webDriver.FindElement(By.Name("toDay"));
            SelectElement selectToDayElement = new SelectElement(toDayElement);
            selectToDayElement.SelectByValue("14");


            IList<IWebElement> seviceClassElements = webDriver.FindElements(By.Name("servClass"));
            //initial value
            text = "";
            foreach (IWebElement element in seviceClassElements)
            {
                text = element.GetAttribute("value");
                if (text.Equals("Business"))
                    element.Click();
            }

            //IWebElement airlineElement = webDriver.FindElement(By.Name("airline"));
            //SelectElement selectAirlineElement = new SelectElement(airlineElement);
            // selectAirlineElement.SelectByValue("Blue Skies Airlines");

            webDriver.FindElement(By.Name("airline")).Click();
            {
                var dropdown = webDriver.FindElement(By.Name("airline"));
                dropdown.FindElement(By.XPath("//option[. = 'Unified Airlines']")).Click();
            }
            webDriver.FindElement(By.CssSelector("tr:nth-child(10) option:nth-child(3)")).Click();

            var js = (IJavaScriptExecutor)webDriver;

            js.ExecuteScript("window.scrollTo(0,344)");

            webDriver.FindElement(By.Name("findFlights")).Click();

        }




        [TearDown]
        public void CleanUp()
        {
            webDriver.Close();
        }


        private static IEnumerable<String[]> ReadDatafromCSV()
        {
            FileStream fileStream = new FileStream("G:/Local disk/TDD/data/logindata.csv",
                FileMode.Open,FileAccess.Read);
            String data1, data2;
            using (var csv = new CsvReader(new StreamReader(fileStream), true))
            {
                while (csv.ReadNextRecord())
                {
                    data1 = csv[0];
                    data2 = csv[1];
                    yield return new[] { data1, data2 };
                }
            }
        }


    }
}
