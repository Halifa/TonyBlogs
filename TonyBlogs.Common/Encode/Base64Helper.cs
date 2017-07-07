using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.Common
{
    public static class Base64Helper
    {
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Encode(string input, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var inputArray = encoding.GetBytes(input);

            var result = Convert.ToBase64String(inputArray);

            return result;
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Encode(byte[] input, Encoding encoding)
        {
            var result = Convert.ToBase64String(input);

            return result;
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Encode(string input, string encoding = "utf-8")
        {
            var coding = Encoding.GetEncoding(encoding);
            return Base64Encode(input, coding);
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Decode(string input, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var outputArray = Convert.FromBase64String(input);

            var result = encoding.GetString(outputArray);

            return result;
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Decode(string input, string encoding = "utf-8")
        {
            var coding = Encoding.GetEncoding(encoding);
            return Base64Decode(input, coding);
        }
    }
}
