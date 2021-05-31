using Microsoft.AspNetCore.Mvc;
using RuilWinkelVaals.ViewModel.RestorePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilWinkelVaals.Models;
using RuilWinkelVaals.Services;
using RuilWinkelVaals.Services.EmailService;
using Microsoft.Extensions.Configuration;

namespace RuilWinkelVaals.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly DB_DevOpsContext db = new DB_DevOpsContext();
        private readonly IConfiguration configuration;

        public ForgotPasswordController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Method to send a password forgotten e-mail
        /// </summary>
        /// <param name="model">Model to send e-mail about forgotten password</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ForgotPassword([Bind("emailAddress, password")] ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = db.ProfileData.Where(e => e.Email == model.emailAddress).FirstOrDefault();
                if(user != null)
                {

                    string token = TokenProviderService.GenerateToken();
                    var salt = db.AccountData.Where(e => e.ProfileId == user.Id).FirstOrDefault();
                    string encryptedToken = EncryptionDecryptionService.Encrypt(token, user.Email, salt.Salt);
                    PasswordForgottenEmail.SendPasswordForgottenEmail(user, encryptedToken, configuration);
                    TempData["Email"] = model.emailAddress;

                    return RedirectToAction("ForgotPasswordConfirmation", "ForgotPassword");
                }
                else
                {
                    TempData["Email"] = model.emailAddress;
                    return RedirectToAction("ForgotPasswordConfirmation", "ForgotPassword");
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// This page will always be shown because of security reasons
        /// </summary>
        /// <returns></returns>
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        public IActionResult ResetPassword([FromQuery(Name ="email")]string email, [FromQuery(Name = "token")]string token)
        {
            return View();
        }
    }
}
