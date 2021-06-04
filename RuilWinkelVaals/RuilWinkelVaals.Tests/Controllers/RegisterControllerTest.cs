using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RuilWinkelVaals.Controllers;
using Microsoft.AspNetCore.Mvc;
using RuilWinkelVaals.ViewModel;
using System.Collections.Specialized;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using RuilWinkelVaals.Tests.Helpers;

namespace RuilWinkelVaals.Tests.Controllers
{
    [TestClass]
    public class RegisterControllerTest
    {
        private readonly ILogger<RegisterController> _logger;
        [TestMethod]
        public void Register()
        {
            //Arange
            RegisterController regControler = new RegisterController(_logger);
            //Act
            ViewResult result = regControler.Register() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

      /*  [TestMethod]
        public void NoEmailFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register();
            newAccount.Email = "";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            ControllerValidationHelper.BindViewModel(regController, newAccount);
            //Act
            ViewResult result = regController.Register(newAccount) as ViewResult;
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["Email"].Errors[0].ErrorMessage, "Er is geen e-mailadres ingevuld");
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            ControllerValidationHelper.BindViewModel(regController, newAccount);
            //Act
            ViewResult result = regController.Register(newAccount) as ViewResult;
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["Password"].Errors[0].ErrorMessage, "Er is geen wachtwoord ingevuld");
        }

        [TestMethod]
        public void NoValidationPasswordFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            ControllerValidationHelper.BindViewModel(regController, newAccount);
            //Act
            ViewResult result = regController.Register(newAccount) as ViewResult;
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["ValidationPassword"].Errors[0].ErrorMessage, "Er is geen wachtwoord ingevuld");
        }

       /* [TestMethod]
        public void NoDoBFilledIn()
        {
            //Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = null;

            ControllerValidationHelper.BindViewModel(regController, newAccount);
            //Act
            ViewResult result = regController.Register(newAccount) as ViewResult;
            //Assert
            Assert.IsTrue(!result.ViewData.ModelState.IsValid);
            Assert.AreEqual(result.ViewData.ModelState["Geboortedatum"].Errors[0].ErrorMessage, "Er is geen geboortedatum ingevuld");
        }

        [TestMethod]
        public void EmailExists()
        {
            // Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register()
            {
                Email = "test",
                Password = "testpassword",
                ValidationPassword = "testpassword",
                Geboortedatum = new DateTime(2000, 01, 01),
            };

            Register existAccount = new Register()
            {
                Email = "test",
                Password = "testpassword",
                ValidationPassword = "testpassword",
                Geboortedatum = new DateTime(2000, 01, 01),
            };

            //Act
            bool AreEqual = newAccount.Email.Equals(existAccount.Email);
            //Assert
            Assert.IsTrue(AreEqual);
        }

        [TestMethod]
        public void Ageunder16()
        {
            // Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword";
            newAccount.ValidationPassword = "testpassword";
            newAccount.Geboortedatum = DateTime.Today.Date;

            //Act
            int result = regController.GetAge(Convert.ToDateTime(newAccount.Geboortedatum));
            //Assert
            if (result < 16)
            {
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void PasswordsNotEqual()
        {
            // Arrange
            RegisterController regController = new RegisterController(_logger);
            Register newAccount = new Register();
            newAccount.Email = "testmail@mail.com";
            newAccount.Password = "testpassword1";
            newAccount.ValidationPassword = "testpassword2";
            newAccount.Geboortedatum = new DateTime(2000, 1, 1);

            //Act
            bool AreEqual = newAccount.Password.Equals(newAccount.ValidationPassword);
            //Assert
            Assert.IsFalse(AreEqual);
        }*/
    }
}