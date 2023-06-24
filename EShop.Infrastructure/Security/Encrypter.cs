using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Security
{
    public class Encrypter : IEncrypter
    {
        public string GetHash(string value, string salt)
        {
            var derivedBytes = new Rfc2898DeriveBytes(value,GetBytes(salt), 1000);
            return Convert.ToBase64String(derivedBytes.GetBytes(50));
        }

        public string GetSalt()
        {
            var saltBytes = new Byte[50];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
