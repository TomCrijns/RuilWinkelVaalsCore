using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;
using System;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class ForgotPasswordUITest
    {

        [TestMethod]
        public void Test()
        {
            /* var chromeOptions = new ChromeOptions();
             chromeOptions.AddArguments("headless");
             //Arange
             string url = "https://www.google.com";
             //ChromeDriver driver = new ChromeDriver(@"C:\Users\tapcr\source\repos\RuilWinkelVaalsCore\RuilWinkelVaals\RuilWinkelVaals.UITests\bin\Debug\netcoreapp2.1");
             ChromeDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
             //Act*/
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            // url = "https://www.google.com";

            string url = "https://www.google.com";
            //using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Notice navigation is slightly different than the Java version
                //This is because 'get' is a keyword in C#
                driver.Navigate().GoToUrl("http://www.google.com/");

                // Find the text input element by its name
                IWebElement query = driver.FindElement(By.Name("q"));

                // Enter something to search for
                query.SendKeys("Cheese");

                // Now submit the form. WebDriver will find the form for us from the element
                query.Submit();

                // Google's search is rendered dynamically with JavaScript.
                // Wait for the page to load, timeout after 10 seconds
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));

                // Should see: "Cheese - Google Search" (for an English locale)
                Assert.AreEqual(driver.Title, "Cheese - Google Search");
            }
        }
    }
}
