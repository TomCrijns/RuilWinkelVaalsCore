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
using RuilWinkelVaals.BusinessLogic.Authentication;

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

        /// <summary>
        /// Page to show when link has expired
        /// </summary>
        /// <returns></returns>
        public IActionResult ForgotPasswordError()
        {
            return View();
        }
        /// <summary>
        /// Show password reset page
        /// </summary>
        /// <param name="email">e-mail address of useraccount for which password will be changed </param>
        /// <param name="token">Token that has been provided by creation of password forgotten e-mail</param>
        /// <returns>A redirect to the Forgotpassword page or the page in which you can reset your password</returns>
        public IActionResult ResetPassword([FromQuery(Name ="email")]string email, [FromQuery(Name = "token")]string token)
        {
            if(email != null || token != null)
            {
                var user = db.ProfileData.Where(e => e.Email == email).FirstOrDefault();
                if(user != null)
                {
                    var salt = db.AccountData.Where(e => e.ProfileId == user.Id).FirstOrDefault();
                    var decryptedToken = EncryptionDecryptionService.Decrypt(token, user.Email, salt.Salt);
                    if(decryptedToken != "Invalid")
                    {
                        DateTime dateTime = TokenProviderService.GetDateTime(decryptedToken);

                        if(dateTime > DateTime.UtcNow.AddHours(-1))
                        {
                            TempData["Email"] = email;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("ForgotPassword");
                        }
                    }
                    else
                    {
                        return RedirectToAction("ForgotPassword");
                    }
                }
                else
                {
                    return RedirectToAction("ForgotPassword");
                }
            }
            else
            {
                return RedirectToAction("ForgotPassword");
            }
        }

        [HttpPost]
        public IActionResult ResetPassword([Bind("password, passwordValidation")] ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["Email"].ToString();
                if(model.password != model.passwordValidation)
                {
                    ModelState.AddModelError("PasswordResetError", "De ingevulde wachtwoorden zijn niet gelijk aan elkaar");
                    return View();
                }
                else
                {
                    var userData = db.ProfileData.Where(e => e.Email == email).FirstOrDefault();
                    if(userData != null)
                    {
                        HashSalt newPassword = HashSalt.GenerateHashSalt(16, model.password);
                        var userCredentials = db.AccountData.Where(user => user.ProfileId == userData.Id).FirstOrDefault();
                        userCredentials.Hash = newPassword.hash;
                        userCredentials.Salt = newPassword.salt;
                        db.SaveChanges();
                        return RedirectToAction("ResetPasswordConfirmation");
                    }
                    else
                    {
                        return RedirectToAction("ForgotPassword");
                    }
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
