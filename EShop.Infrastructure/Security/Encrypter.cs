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
        private const string Salt = "@OweXpH+?7zjFLA=doXNdKsG-u2#d(zehHIMcqeC66ZU?zTx:K";
       
        public string GetHash(string value)
        {
            var derivedBytes = new Rfc2898DeriveBytes(value, GetBytes(Salt), 1000);
            var hash = Convert.ToBase64String(derivedBytes.GetBytes(50));
            return hash;
        }
        public string GetSalt()
        {
            //var saltBytes = new Byte[50];
            //var range = RandomNumberGenerator.Create();
            //range.GetBytes(saltBytes);
            //return Convert.ToBase64String(saltBytes);
            return Salt;
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
