using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class EdgeDriverTest
    {
        /*// In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.
        
        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);
            _driver1 = new FirefoxDriver(options);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://www.bing.com";
            Assert.AreEqual("Bing", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }*/
        [TestMethod]
        public void TestMethod1()
        {
            string url = "<URL>";
            ChromeDriver driver = new ChromeDriver(@"<DRIVER_PATH_FULL_LOCAL_PC");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Your code

            //This is example code which can be used for own testing
            //Make sure everything has clear classname or id


            /*driver.FindElement(By.ClassName("Textbox")).SendKeys("admin@ruilwinkelvaals.nl");
            driver.FindElement(By.ClassName("Textbox")).SendKeys("admin");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 10));
            wait.Until(wt => wt.FindElement(By.ClassName("Validation")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("Er is geen wachtwoord ingevuld"));*/

            driver.Close();
            driver.Dispose();
        }

    }
}
