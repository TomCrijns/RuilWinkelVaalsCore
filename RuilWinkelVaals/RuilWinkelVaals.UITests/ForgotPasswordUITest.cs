using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;
using System;
using RuilWinkelVaals.Services;

namespace RuilWinkelVaals.UITests
{
    [TestClass]
    public class ForgotPasswordUITest
    {
        [TestMethod]
        public void GoToForgotPasswordPage()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.TagName("a")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.ClassName("SendRMailLabel")));
                var text = driver.FindElement(By.ClassName("SendRMailLabel"));

                //Assert
                Assert.IsTrue(text.Text.Contains("Voer hieronder uw e-mailadres in zodat wij u een link naar de wachtwoord herstelpagina kunnen sturen."));
            }
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ForgotPassword";
            using(var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.ClassName("Validation")));
                var text = driver.FindElement(By.ClassName("Validation"));

                //Assert
                Assert.IsTrue(text.Text.Contains("ER IS GEEN E-MAILADRES INGEVULD"));
            }
        }

        [TestMethod]
        public void EmailSendConfirmation()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ForgotPassword";
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("EmailTextBox")).SendKeys("test@test.com");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var text = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(text.Text.Contains("GELUKT"));
            }
        }

        [TestMethod]
        public void ShowResetPasswordPage()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "1816802crijns@zuyd.nl";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, email, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);
         
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
                wait.Until(wt => wt.FindElement(By.ClassName("Label")));
                var text = driver.FindElement(By.ClassName("Label"));

                //Assert
                Assert.IsTrue(text.Text.Contains("WACHTWOORD"));
            }
        }

        [TestMethod]
        public void NoPasswordsFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "1816802crijns@zuyd.nl";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, email, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
                wait.Until(wt => wt.FindElement(By.ClassName("Validation")));
                var validation = driver.FindElement(By.ClassName("Validation"));
                var passwordValidationValidation = driver.FindElement(By.Id("PasswordValidationValidation"));

                //Assert
                Assert.IsTrue(validation.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
                Assert.IsTrue(passwordValidationValidation.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
            }
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "1816802crijns@zuyd.nl";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, email, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("PasswordValidationTextBox")).SendKeys("T3stW@achtw00rD");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.ClassName("Validation")));
                var passwordValidation = driver.FindElement(By.ClassName("Validation"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
            }
        }

        [TestMethod]
        public void PasswordsNotEqual()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "1816802crijns@zuyd.nl";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, email, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("T3stW@achtw00rD1");
                driver.FindElement(By.Id("PasswordValidationTextBox")).SendKeys("T3stW@achtw00rD");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.Id("PasswordValidationError")));
                var passwordValidation = driver.FindElement(By.Id("PasswordValidationError"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("DE INGEVULDE WACHTWOORDEN ZIJN NIET GELIJK AAN ELKAAR"));
            }
        }

        [TestMethod]
        public void SuccesfullPasswordReset()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "1816802crijns@zuyd.nl";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, email, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("T3stW@achtw00rD");
                driver.FindElement(By.Id("PasswordValidationTextBox")).SendKeys("T3stW@achtw00rD");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("GELUKT"));
            }
        }

        [TestMethod]
        public void NoEmailAddressOrToken()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "";
            string encryptedToken = "";
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ONGELDIGE LINK"));
            }
        }

        [TestMethod]
        public void NoEmailAddress()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string fakeEmailForEncryption = "test@test.nl";
            string email = "";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, fakeEmailForEncryption, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ONGELDIGE LINK"));
            }
        }

        [TestMethod]
        public void NoToken()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var encryptedToken = "";
            string email = "1816802crijns@zuyd.nl";
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ONGELDIGE LINK"));
            }
        }

        [TestMethod]
        public void NonExistingEmailAddress()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = TokenProviderService.GenerateToken();
            string email = "Test@Test.nl";
            string salt = DBDataHelper.GetSaltFromDB();
            var encryptedToken = EncryptionDecryptionService.Encrypt(token, email, salt);
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, encryptedToken);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ONGELDIGE LINK"));
            }
        }

        [TestMethod]
        public void InvalidToken()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = "50396ek4mfwldlti";
            string email = "1816802crijns@zuyd.nl";
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, token);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ONGELDIGE LINK"));
            }
        }

        [TestMethod]
        public void ExpiredToken()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var token = "OJEZqAi8ATk2+JynZbTTocYyMRaRLhmzcM1uN0+Wb1d/SYvBNl3OQg==";
            string email = "1816802crijns@zuyd.nl";
            string url = String.Format("https://test-ruilwinkelvaalscore.azurewebsites.net/ForgotPassword/ResetPassword?email={0}&token={1}", email, token);

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));
                var passwordValidation = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ONGELDIGE LINK"));
            }
        }
    }
}
