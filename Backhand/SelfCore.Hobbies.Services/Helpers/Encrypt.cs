using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SelfCore.Hobbies.Services.Helpers
{
    /// <summary>
    /// 加密/解密操作
    /// </summary>
    public static class Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string value, Encoding encoding) {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            string result = "";
            var md5 = new MD5CryptoServiceProvider();
            try {
                byte[] bys = md5.ComputeHash(encoding.GetBytes(value));
                result = BitConverter.ToString(bys).Replace("-","");
                return result;
            }
            catch { return result; }
        }
    }
}
