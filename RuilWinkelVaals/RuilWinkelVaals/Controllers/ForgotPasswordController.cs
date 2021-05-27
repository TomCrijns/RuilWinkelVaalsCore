using Microsoft.AspNetCore.Mvc;
using RuilWinkelVaals.ViewModel.RestorePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilWinkelVaals.Models;
using RuilWinkelVaals.Services;
using RuilWinkelVaals.Services;
using RuilWinkelVaals.Services.EmailService;

namespace RuilWinkelVaals.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly DB_DevOpsContext db = new DB_DevOpsContext();

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword([Bind("emailAddress, password")] ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = db.ProfileData.Where(e => e.Email == model.emailAddress).FirstOrDefault();
                if(user != null)
                {
                    PasswordForgottenEmail.SendPasswordForgottenEmail();
                    string token = TokenProviderService.GenerateToken();
                    var salt = db.AccountData.Where(e => e.ProfileId == user.Id).FirstOrDefault();
                    string encryptedToken = EncryptionDecryptionService.Encrypt(token, user.Email, salt.Salt);

                    string decryptedToken = EncryptionDecryptionService.Decrypt(encryptedToken, user.Email, salt.Salt);
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

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        public IActionResult RestorePassword([FromQuery(Name ="email")]string email, [FromQuery(Name = "token")]string token)
        {
            return View();
        }
    }
}
