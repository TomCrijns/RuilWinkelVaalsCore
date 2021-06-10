using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuilWinkelVaals.Controllers;
using RuilWinkelVaals.Tests.Helpers;
using RuilWinkelVaals.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuilWinkelVaals.ViewModel.RestorePassword;
namespace RuilWinkelVaals.Tests.Controllers
{
    [TestClass]
    public class ForgotPasswordControllerTest
    {
        private readonly IConfiguration config;
        [TestMethod]
        public void ForgotPassword()
        {
            ForgotPasswordController controller = new ForgotPasswordController(config);
            ViewResult result = controller.ForgotPassword() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ForgotPasswordConfirmation()
        {
            ForgotPasswordController controller = new ForgotPasswordController(config);
            ViewResult result = controller.ForgotPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NoEmailFilledin()
        {
            ForgotPasswordController controller = new ForgotPasswordController(config);
            ForgotPassword forgotPassword = new ForgotPassword()
            {
                emailAddress = ""
            };

            ControllerValidationHelper.BindViewModel(controller, forgotPassword);
            ViewResult result = controller.ForgotPassword() as ViewResult;
            Assert.AreEqual("Er is geen e-mailadres ingevuld", result.ViewData.ModelState["emailAddress"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void ForgotPasswordError()
        {
            ForgotPasswordController controller = new ForgotPasswordController(config);
            ViewResult result = controller.ForgotPasswordError() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ResetPasswordConfirmation()
        {
            ForgotPasswordController controller = new ForgotPasswordController(config);
            ViewResult result = controller.ResetPasswordConfirmation() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
