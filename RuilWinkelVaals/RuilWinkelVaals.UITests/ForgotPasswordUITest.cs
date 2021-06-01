using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class ForgotPasswordUITest
    {
        [TestMethod]
        public void Test()
        {
            //Arange
            string url = "https://www.google.com";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            //Act
            driver.Navigate().GoToUrl(url);
        }
    }
}
