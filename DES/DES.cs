//Josh Hickman
//Cryptography
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Random Number DES


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

                int original = r.Next(0, 255);  //Begins with a random number from Random();

                using (DESCryptoServiceProvider myDES = new DESCryptoServiceProvider())
                {
                    Console.WriteLine("Random Number:   {0}", original);
                    byte[] encrypted = Encrypt(original, myDES.Key, myDES.IV);  //Byte array from encrypted Int
                    num = Convert.ToInt32(encrypted.Last());                    //Converted byte array back into an Int, giving the first Crypto RNG

                    for (int i = 0; i < 30; i++)        //For loop to finish the sequence
                    {
                        encrypted = Encrypt(num, myDES.Key, myDES.IV);
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

            using (DESCryptoServiceProvider desAlg = new DESCryptoServiceProvider())
            {
                desAlg.Key = Key;
                desAlg.IV = IV;

                ICryptoTransform encryptor = desAlg.CreateEncryptor(desAlg.Key, desAlg.IV);

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
