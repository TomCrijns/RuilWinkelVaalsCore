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
    }
}
