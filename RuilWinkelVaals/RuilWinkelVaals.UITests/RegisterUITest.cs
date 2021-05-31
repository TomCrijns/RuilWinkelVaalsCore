using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text;
using System;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class RegisterUITest
    {
        //Method for making RandomEmails
        public string RandomEmailGenerator()
        {
            var generator = new Random();
            bool lowerCase = false;
            int size = generator.Next(0, 15);
            
            var builder = new StringBuilder();
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)generator.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            string randomstring = builder.ToString().ToLower();
            string email = randomstring + "@testmail.com";
            return email;
        }

        [TestMethod]
        public void DoBToYoung()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys(Convert.ToString(DateTime.Today.Date));
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password1");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password2");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("U dient minimaal 16jaar te zijn om te registreren."));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void EmailExists()
        {
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys("admin@ruilwinkelvaals.nl");
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("01-01-2000");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password1");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password2");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("Er bestaat al een account met dit Email adres."));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void PasswordNotEqual()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("01-01-2000");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password1");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password2");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("De gegeven wachtwoorden komen niet overeen met elkaar."));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys("");
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("01-01-2000");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("Er is geen e-mailadres ingevuld"));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("01-01-2000");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("Er is geen wachtwoord ingevuld"));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void NoValidationPasswordFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("01-01-2000");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("Er is geen wachtwoord ingevuld"));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void NoDoBFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.Id("registrationError")));
            var message = driver.FindElement(By.ClassName("field-validation-error"));
            Assert.IsTrue(message.Text.Contains("Er is geen geboortedatum ingevuld"));

            driver.Close();
            driver.Dispose();
        }

        [TestMethod]
        public void SuccessNewRegistration()
        {
            string randomEmail = RandomEmailGenerator();
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Register/Register";
            FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //Input fields
            driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
            driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
            driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
            driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
            driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
            driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
            driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
            driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("01-01-2000");
            driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
            driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

            //Submit button
            driver.FindElement(By.Id("registerButton")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

            wait.Until(wt => wt.FindElement(By.ClassName("display-4")));
            var message = driver.FindElement(By.ClassName("display-4"));
            Assert.IsTrue(message.Text.Contains("Welcome"));

            driver.Close();
            driver.Dispose();
        }
    }
}
