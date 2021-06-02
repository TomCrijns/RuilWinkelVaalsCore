using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class ForgotPasswordUITest
    {

        [TestMethod]
        public void Test()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            //Arange
            string url = "https://www.google.com";
            //ChromeDriver driver = new ChromeDriver(@"C:\Users\tapcr\source\repos\RuilWinkelVaalsCore\RuilWinkelVaals\RuilWinkelVaals.UITests\bin\Debug\netcoreapp2.1");
            ChromeDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
            //Act
            driver.Navigate().GoToUrl(url);
        }
    }
}
