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
                AccountDatum userCredentials = new AccountDatum();
                var profileData = db.ProfileData.Where(user => user.Email == model.emailAddress).FirstOrDefault();
                if (profileData != null)
                {
                    userCredentials = db.AccountData.Where(user => user.ProfileId == profileData.Id).FirstOrDefault();
                    authenticated = Authentication.AuthenticateUser(model.emailAddress, profileData.Email, model.password, userCredentials.Hash, userCredentials.Salt);
                }
                if (userCredentials.Blocked == true)
                {
                    //Useraccount blocked status is true, access denied
                    ModelState.AddModelError("LoginError", "account is geblokkeerd");
                    return View();
                }
                else if (authenticated)
                {
                        //UserAccount blocked status is false, access granted
                        HttpContext.Session.SetInt32("UserId", profileData.Id);
                        HttpContext.Session.SetInt32("AccountType", Int32.Parse(profileData.AccountType.ToString()));
                        HttpContext.Session.SetString("ProfileName", profileData.Voornaam);
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    //If the given Mailadres is not equal to the previous mailadres
                    if(HttpContext.Session.GetString("MailAdres") != model.emailAddress)
                    {
                        HttpContext.Session.SetString("MailAdres", model.emailAddress);
                        HttpContext.Session.SetInt32("BlockedCounter", 1);
                        ModelState.AddModelError("LoginError", "De gebruikersnaam/wachtwoord is incorrect");
                        return View();
                    }
                    //Current Mail adres and previous mail adres are equal
                    //Add 1 o BlockedCounter
                    int BlockedValue = Convert.ToInt32(HttpContext.Session.GetInt32("BlockedCounter")) +1;
                    HttpContext.Session.SetInt32("BlockedCounter", BlockedValue);

                    //Check if BlockedValue >= 3, user will get blocked
                    if(BlockedValue >= 3)
                    {
                        //AccountData change to blocked and save changes
                        userCredentials.Blocked = true;
                        userCredentials.DateBlocked = DateTime.Now.Date;
                        db.AccountData.Update(userCredentials);
                        db.SaveChanges();

                        ModelState.AddModelError("LoginError", "Geblokkeerd, neem contact op met de klantenservice");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("LoginError", "De gebruikersnaam/wachtwoord is incorrect");

                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
    }
}
