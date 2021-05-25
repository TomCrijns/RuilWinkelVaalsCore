using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuilWinkelVaals.BusinessLogic.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Tests.BusinessLogic.Authentication
{
    [TestClass]
    public class HashSaltTest
    {
        [TestMethod]
        public void GeneratePassword()
        {
            //Arange
            HashSalt generatedHashAndSalt = new HashSalt();

            //Act
            generatedHashAndSalt = HashSalt.GenerateHashSalt(16, "password");

            //Assert
            Assert.IsNotNull(generatedHashAndSalt);
        }
    }
}
