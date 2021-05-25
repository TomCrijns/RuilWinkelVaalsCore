using Microsoft.AspNetCore.Mvc;
using RuilWinkelVaals.ViewModel.RestorePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword([Bind("emailAddress, password")] ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                TempData["Email"] = model.emailAddress;
                return RedirectToAction("ForgotPasswordConfirmation", "ForgotPassword");
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
