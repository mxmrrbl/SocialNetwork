using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password)
        {
            using(SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sb = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }

                return sb.ToString();
            }

            
        }
    }
}
