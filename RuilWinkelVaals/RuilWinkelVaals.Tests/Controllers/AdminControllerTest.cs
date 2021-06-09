using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuilWinkelVaals.Controllers;

namespace RuilWinkelVaals.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private readonly ILogger<HomeController> _logger;
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController(_logger);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
