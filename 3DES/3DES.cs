//Josh Hickman
//Cryptography 
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Random Number 3DES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading;

namespace DES
{
    class DES
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            String input;
            int num;

            try
            {

                int original = r.Next(0, 255);                  //Begins with a random number from Random();

                using (TripleDESCryptoServiceProvider myTripleDES = new TripleDESCryptoServiceProvider())
                {
                    Console.WriteLine("Random Number:   {0}", original);                        //Byte array from encrypted Int
                    byte[] encrypted = Encrypt(original, myTripleDES.Key, myTripleDES.IV);      //Converted byte array back into an Int, giving the first Crypto RNG
                    num = Convert.ToInt32(encrypted.Last());

                    for (int i = 0; i < 30; i++)        //For loop to finish the sequence
                    {
                        encrypted = Encrypt(num, myTripleDES.Key, myTripleDES.IV);
                        num = Convert.ToInt32(encrypted.Last());

                        Console.WriteLine("Sequence {0}: {1}", i, num);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            input = Console.ReadLine();

        }
        static byte[] Encrypt(int plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (TripleDESCryptoServiceProvider tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = Key;
                tdsAlg.IV = IV;

                ICryptoTransform encryptor = tdsAlg.CreateEncryptor(tdsAlg.Key, tdsAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            } 
            return encrypted;
        }
    }
}
