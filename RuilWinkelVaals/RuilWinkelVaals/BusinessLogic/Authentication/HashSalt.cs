using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RuilWinkelVaals.BusinessLogic.Authentication
{
    public class HashSalt
    {
        public string hash { get; set; }
        public string salt { get; set; }

        /// <summary>
        /// This method generates a hash and a salt for the filled in password which provides extra security
        /// </summary>
        /// <param name="size">Length is salt</param>
        /// <param name="password">Password filled in by user</param>
        /// <returns></returns>
        public static HashSalt GenerateHashSalt(int size, string password)
        {
            //Generate salt
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            //Generate hash
            var rfc28898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc28898DeriveBytes.GetBytes(256));

            //Store hash & salt in hashSalt object that gets returned to requester
            HashSalt hashSalt = new HashSalt { hash = hashPassword, salt = salt };
            return hashSalt;
        }
    }
}
