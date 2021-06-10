using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuilWinkelVaals.Controllers;

namespace RuilWinkelVaals.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        private readonly ILogger<HomeController> _logger;
        /*[TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController(_logger);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }*/

        [TestMethod]
        public void Privacy()
        {
            HomeController controller = new HomeController(_logger);
            ViewResult result = controller.Privacy() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
