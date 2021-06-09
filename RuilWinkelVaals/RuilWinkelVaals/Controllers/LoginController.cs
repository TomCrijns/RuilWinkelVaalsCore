using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuilWinkelVaals.BusinessLogic.Authentication;
using RuilWinkelVaals.Models;
using RuilWinkelVaals.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Controllers
{
    public class LoginController : Controller
    {
        private readonly DB_DevOpsContext db = new DB_DevOpsContext();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("emailAddress, password")] Login model)
        {
            if (ModelState.IsValid)
            {
                bool authenticated = false;
                var profileData = db.ProfileData.Where(user => user.Email == model.emailAddress).FirstOrDefault();
                if (profileData != null)
                {
                    var userCredentials = db.AccountData.Where(user => user.ProfileId == profileData.Id).FirstOrDefault();
                    authenticated = Authentication.AuthenticateUser(model.emailAddress, profileData.Email, model.password, userCredentials.Hash, userCredentials.Salt);
                }
                if (authenticated)
                {
                    HttpContext.Session.SetInt32("UserId", profileData.Id);
                    HttpContext.Session.SetInt32("AccountType", Int32.Parse(profileData.AccountType.ToString()));
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "De gebruikersnaam/wachtwoord is incorrect");

                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
