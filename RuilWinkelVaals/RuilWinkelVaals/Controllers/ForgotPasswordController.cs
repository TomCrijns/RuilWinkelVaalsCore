using Microsoft.AspNetCore.Mvc;
using RuilWinkelVaals.ViewModel.RestorePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuilWinkelVaals.Models;

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
            //var test = email;
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string generatedToken = Convert.ToBase64String(time.Concat(key).ToArray());
            return View();
        }
    }
}
