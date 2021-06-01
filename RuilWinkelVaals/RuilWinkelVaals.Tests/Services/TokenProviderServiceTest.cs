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
using RuilWinkelVaals.Services;

namespace RuilWinkelVaals.Tests.Services
{
    [TestClass]
    public class TokenProviderServiceTest
    {
       [TestMethod]
       public void GenerateToken()
        {
            var token = TokenProviderService.GenerateToken();
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void DecodeToken()
        {
            var token = TokenProviderService.GenerateToken();
            DateTime dateTime = TokenProviderService.GetDateTime(token);
            Assert.IsNotNull(token);
        }
    }
}
