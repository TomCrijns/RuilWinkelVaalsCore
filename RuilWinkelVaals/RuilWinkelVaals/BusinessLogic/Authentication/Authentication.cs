using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RuilWinkelVaals.BusinessLogic.Authentication
{
    public class Authentication
    {
        public string password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typedUserName">Username filled in by user</param>
        /// <param name="userName">Username that's stored in database</param>
        /// <param name="password">Password filled in by user</param>
        /// <param name="hash">Password hash stored in database</param>
        /// <param name="salt">Salt that belongs to stored password</param>
        /// <returns></returns>
        public static bool AuthenticateUser(string typedUserName, string userName, string password, string hash, string salt)
        {
            //Turn filled in password to hash
            var saltBytes = Convert.FromBase64String(salt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);

            //Check if there's a match between stored password and filled in password
            bool isPasswordMatch = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == hash;

            if (userName == typedUserName && isPasswordMatch)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
