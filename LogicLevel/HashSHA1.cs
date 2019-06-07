using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public class HashSHA1 : IHashAlgorithm
    {
        public byte[] GetHash(params string[] strings)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(
                strings.Aggregate((result, str)
                => $"{result}{(string.IsNullOrEmpty(result) ? string.Empty : " | ")}{str}")));
        }
    }
}
