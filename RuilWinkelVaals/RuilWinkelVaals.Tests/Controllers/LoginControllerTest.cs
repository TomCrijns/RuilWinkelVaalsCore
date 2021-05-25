using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuilWinkelVaals.Controllers;
using RuilWinkelVaals.Tests.Helpers;
using RuilWinkelVaals.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Login()
        {
            LoginController controller = new LoginController();
            ViewResult result = controller.Login() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CorrectLogin()
        {
            //Arange
            Login loginAccount = new Login();
            loginAccount.emailAddress = "testuser1@ruilwinkelvaals.nl";
            loginAccount.password = "test2021";

            //Act
            bool authenticated = RuilWinkelVaals.BusinessLogic.Authentication.Authentication.AuthenticateUser(
                loginAccount.emailAddress,
                "testuser1@ruilwinkelvaals.nl",
                loginAccount.password,
                "zPcuh9FQGBfNatOO118OM8UtIP0sTFriDJd/MegmC3hPn8s5rs880mArdmFfYToHRAb3raQLNwoFhdkN4AoIh1BuFyeKvoscQqFpFa4y23KmjaSVk4uDTC0hk5dr8SpU4jVlySfD3iP0b/cifnyNlA3Xe/EsYVuY+V7v2xiB0BQTnBub+ly+bA4jY9FSVmUgg/3qqdTAK8wpfjs9z5GpkbgbnSTKzPwEdeJLHnPxd5X9D92X2Auaqyjd3ABn55oR5r8EsBURhtexy1zBi8RBCqDopFCez7O6DpbMJfCwEy4pmcSy/Yil4NGcdDHeXrmM5KM9G3eN0QhOqxjGUqId5w==",
                "X9hY/UwgcVcOpBN+pgmL3Q==");

            //Assert
            Assert.IsTrue(authenticated);
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            LoginController controller = new LoginController();
            Login login = new Login()
            {
                emailAddress = "test",
                password = ""
            };

            ControllerValidationHelper.BindViewModel(controller, login);
            ViewResult result = controller.Login(login) as ViewResult;
            Assert.AreEqual("Er is geen wachtwoord ingevuld", result.ViewData.ModelState["password"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            LoginController controller = new LoginController();
            Login login = new Login()
            {
                emailAddress = "",
                password = "admin"
            };

            ControllerValidationHelper.BindViewModel(controller, login);
            ViewResult result = controller.Login(login) as ViewResult;
            Assert.AreEqual("Er is geen e-mailadres ingevuld", result.ViewData.ModelState["emailAddress"].Errors[0].ErrorMessage);
        }
    }
}
