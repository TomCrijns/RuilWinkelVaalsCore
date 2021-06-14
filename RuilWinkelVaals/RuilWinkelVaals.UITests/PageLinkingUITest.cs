using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class PageLinkingUITest
    {
        [TestMethod]
        public void RegisterLink()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("RegisterLink")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var text = driver.FindElement(By.TagName("h1"));
                //Assert
                Assert.IsTrue(text.Text.Contains("Admin Panel"));
            }
        }
    }
}
