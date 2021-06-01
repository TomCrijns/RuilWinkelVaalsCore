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
    public class EncryptionDecryptionServiceTest
    {
        [TestMethod]
        public void GenerateEncryptedString()
        {
            string unEncryptedString = "63fu6929r40w";
            string email = "testuser1@ruilwinkelvaals.nl";
            string salt = "5574m5w==";

            var encryptedString = EncryptionDecryptionService.Encrypt(unEncryptedString, email, salt);
            Assert.IsNotNull(encryptedString);
        }

        [TestMethod]
        public void DecryptEncryptedString()
        {
            string unEncryptedString = "63fu6929r40w";
            string email = "testuser1@ruilwinkelvaals.nl";
            string salt = "5574m5w==";

            var encryptedString = EncryptionDecryptionService.Encrypt(unEncryptedString, email, salt);
            var decryptedString = EncryptionDecryptionService.Decrypt(encryptedString, email, salt);
            Assert.IsNotNull(decryptedString);
            Assert.AreEqual("63fu6929r40w", decryptedString);
        }
    }
}
