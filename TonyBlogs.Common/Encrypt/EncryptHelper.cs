using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TonyBlogs.Common
{
    public class EncryptHelper
    {
        private static string encryptKey = "6owE4u(7";

        #region ========加密========

        public static string Encrypt(string pToEncrypt)
        {
            return Encrypt(pToEncrypt, encryptKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pToEncrypt">要加密的内容</param>
        /// <param name="sKey">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            des.Key = Encoding.UTF8.GetBytes(sKey);　//建立加密对象的密钥和偏移量
            des.IV = Encoding.UTF8.GetBytes(sKey);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  |||  如果是用ECB模式，则IV不管是什么都不会影响加密/解密的结果

            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);//定义将数据流链接到加密转换的流
            cs.Write(inputByteArray, 0, inputByteArray.Length);//上面已经完成了把加密后的结果放到内存中去
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        ///  DES加密
        /// </summary>
        /// <param name="pToEncrypt">要加密的内容</param>
        /// <param name="sKey">密钥</param>
        /// <param name="StrMs">输出加密后的字符串</param>
        /// <returns>加密后的byte数组</returns>
        public static byte[] Encrypt(string pToEncrypt, string sKey, out string StrMs)
        {
            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            des.Key = Encoding.UTF8.GetBytes(sKey);　//建立加密对象的密钥和偏移量
            //des.IV = Encoding.UTF8.GetBytes(sKey);　 // 如果是用ECB模式，则IV不管是什么都不会影响加密/解密的结果

            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);//定义将数据流链接到加密转换的流
            cs.Write(inputByteArray, 0, inputByteArray.Length);//上面已经完成了把加密后的结果放到内存中去
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            StrMs = ret.ToString();
            return ms.ToArray();
        }
        #endregion

        #region ========解密========

        public static string Decrypt(string pToDecrypt)
        {
            return Decrypt(pToDecrypt, encryptKey);
        }
        /// <summary>
        /// DEC 解密过程
        /// </summary>
        /// <param name="pToDecrypt">被解密的字符串</param>
        /// <param name="sKey">密钥（只支持8个字节的密钥，同前面的加密密钥相同）</param>
        /// <returns>返回被解密的字符串</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.UTF8.GetBytes(sKey);　//建立加密对象的密钥和偏移量，此值重要，不能修改
            //des.IV = Encoding.UTF8.GetBytes(sKey);// 如果是用ECB模式，则IV不管是什么都不会影响加密/解密的结果
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="EncryptedDataArray"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptTextFromMemory(byte[] EncryptedDataArray, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(EncryptedDataArray, 0, EncryptedDataArray.Length);
            cs.FlushFinalBlock();
            ms.Close();
            cs.Close();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strData">要加密的数据</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回Base64编码后的加密字符串</returns>
        public static string EncryptToBase64(string strData, string strKey)
        {
            string strRtn;
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();//des进行加密
                desc.Mode = CipherMode.ECB;
                desc.Padding = PaddingMode.PKCS7;
                byte[] key = System.Text.Encoding.UTF8.GetBytes(strKey);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(strData);
                MemoryStream ms = new MemoryStream();//存储加密后的数据
                CryptoStream cs = new CryptoStream(ms, desc.CreateEncryptor(key, key), CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);//进行加密
                cs.FlushFinalBlock();
                //return System.Text.Encoding.UTF8.GetString(ms.ToArray());//取加密后的数据
                strRtn = Convert.ToBase64String(ms.ToArray());
                return strRtn;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strData">要解密的数据</param>
        /// <param name="strKey">密钥</param>
        /// <returns></returns>
        public static string DecryptFromBase64(string strData, string strKey)
        {
            string strRtn;
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                desc.Mode = CipherMode.ECB;
                desc.Padding = PaddingMode.PKCS7;
                byte[] key = System.Text.Encoding.UTF8.GetBytes(strKey);
                //byte[] data = System.Text.Encoding.UTF8.GetBytes(strData);
                byte[] rdata = Convert.FromBase64String(strData);
                MemoryStream ms = new MemoryStream();//存储解密后的数据
                CryptoStream cs = new CryptoStream(ms, desc.CreateDecryptor(key, key), CryptoStreamMode.Write);
                cs.Write(rdata, 0, rdata.Length);//解密数据
                cs.FlushFinalBlock();
                strRtn = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                return strRtn;
            }
            catch
            {
                return "";
            }
        }
    }
}
