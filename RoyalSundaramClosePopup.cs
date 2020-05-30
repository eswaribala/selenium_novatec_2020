using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWatch
{
    class RoyalSundaramClosePopup
    {
        private IWebDriver webDriver;
        private string url;
        private IDictionary<String, String> dictionary;

        [SetUp]
        public void Init()
        {
            dictionary = ResourceHelper.GetAttributes();
            webDriver = new ChromeDriver(dictionary["driver"].ToString());
            url = dictionary["rsurl"].ToString();

        }

        [Test]
        public void RSClosePopupTest()
        {
            webDriver.Url = url;
            webDriver.Manage().Window.Maximize();
            String title = "";
            //get windows
            foreach(String handle in webDriver.WindowHandles)
            {
                webDriver.SwitchTo().Window(handle);
                WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(2000));
                IWebElement element = wait.Until(drv => drv.FindElement
                (By.ClassName("rsgi-close")));
                if(element.Displayed)
                {
                    element.Click();
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);
                    title = webDriver.FindElement(By.CssSelector("h1[class='welcomenote welcomenote_home']")).Text;
                    Assert.IsTrue(title.Contains("Welcome To"));
                }

            }


        }

        [TearDown]
        public void RSClose()
        {
            webDriver.Close();
        }

    }
}
