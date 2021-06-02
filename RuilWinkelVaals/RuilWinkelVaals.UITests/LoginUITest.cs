using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class LoginUITest
    {
        [TestMethod]
        public void Login()
        {
            //Arange
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
            
            //Act
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser1@ruilwinkelvaals.nl");
            driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
            wait.Until(wt => wt.FindElement(By.TagName("p")));

            var text = driver.FindElement(By.TagName("p"));

            //Assert
            Assert.IsTrue(text.Text.Contains("Use this page to detail your site's privacy policy."));
        }

        [TestMethod]
        public void IncorrectEmail()
        {
            //Arange
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            //Act
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser@ruilwinkelvaals.nl");
            driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
            wait.Until(wt => wt.FindElement(By.Id("LoginValidationError")));

            var text = driver.FindElement(By.Id("LoginValidationError"));

            //Assert
            Assert.IsTrue(text.Text.Contains("DE GEBRUIKERSNAAM/WACHTWOORD IS INCORRECT"));
        }

        [TestMethod]
        public void IncorrectPassword()
        {
            //Arange
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            //Act
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser1@ruilwinkelvaals.nl");
            driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021!");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
            wait.Until(wt => wt.FindElement(By.Id("LoginValidationError")));

            var text = driver.FindElement(By.Id("LoginValidationError"));

            //Assert
            Assert.IsTrue(text.Text.Contains("DE GEBRUIKERSNAAM/WACHTWOORD IS INCORRECT"));
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            //Arange
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            //Act
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.ClassName("Textbox")).SendKeys("");
            driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
            wait.Until(wt => wt.FindElement(By.ClassName("Validation")));

            var text = driver.FindElement(By.ClassName("Validation"));

            //Assert
            Assert.IsTrue(text.Text.Contains("ER IS GEEN E-MAILADRES INGEVULD"));
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            //Arange
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            //Act
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser1@ruilwinkelvaals.nl");
            driver.FindElement(By.Id("PasswordTextBox")).SendKeys("");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
            wait.Until(wt => wt.FindElement(By.Id("PasswordValidation")));

            var text = driver.FindElement(By.Id("PasswordValidation"));

            //Assert
            Assert.IsTrue(text.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
        }
        [TestMethod]
        public void NoEmailAndPasswordFilledIn()
        {
            //Arange
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            //Act
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.FindElement(By.ClassName("Textbox")).SendKeys("");
            driver.FindElement(By.Id("PasswordTextBox")).SendKeys("");
            driver.FindElement(By.ClassName("Button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
            wait.Until(wt => wt.FindElement(By.Id("PasswordValidation")));

            var emailValidation = driver.FindElement(By.ClassName("Validation"));
            var passwordValidation = driver.FindElement(By.Id("PasswordValidation"));

            //Assert
            Assert.IsTrue(passwordValidation.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
            Assert.IsTrue(emailValidation.Text.Contains("ER IS GEEN E-MAILADRES INGEVULD"));
        }
    }
}
