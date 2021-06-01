using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuilWinkelVaals.Services
{
    public static class TokenProviderService
    {
        /// <summary>
        /// Method to generate a token with a DateTime in it for validation
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string generatedToken = Convert.ToBase64String(time.Concat(key).ToArray());

            return generatedToken;
        }

        /// <summary>
        /// Method to decode a token and extract the DateTime portion of it
        /// If token is invalid, set current date and time with an extraction of 2 hours so that the token is invalid
        /// </summary>
        /// <param name="token">Decrypted token with DateTime not extracted yet</param>
        /// <returns>DateTime of when token was created</returns>
        public static DateTime GetDateTime(string token)
        {
            try
            {
                byte[] tokenData = Convert.FromBase64String(token);
                DateTime dateTime = DateTime.FromBinary(BitConverter.ToInt64(tokenData, 0));
                return dateTime;
            }
            catch
            {
                DateTime dateTime = DateTime.UtcNow.AddHours(-2);
                return dateTime;
            }
        }
    }
}
