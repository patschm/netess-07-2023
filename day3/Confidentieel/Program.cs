using System.Security.Cryptography;
using System.Text;

namespace Confidentieel;
class Program
{
    static void Main(string[] args)
    {
        //TestSymmetrisch();
        TestAsymmetrisch();
        Console.ReadLine();
    }

    private static void TestAsymmetrisch()
    {
        // Ontvanger
        var keyGen = RSA.Create();
        var publicKey = keyGen.ToXmlString(false);
        var privateKey = keyGen.ToXmlString(true);


        // Zender
        string loveLetter = "Hello World";
        var alg = RSA.Create();
        alg.FromXmlString(publicKey);
        byte[] cipher = alg.Encrypt(Encoding.UTF8.GetBytes(loveLetter), RSAEncryptionPadding.Pkcs1);


        // Ontvanger
        var alg2 = RSA.Create();
        alg2.FromXmlString(privateKey);
        byte[] data = alg2.Decrypt(cipher, RSAEncryptionPadding.Pkcs1);
        System.Console.WriteLine(Encoding.UTF8.GetString(data));


    }

    private static void TestSymmetrisch()
    {
        // Zender
        string loveLetter = "Hello World";
        Aes alg = Aes.Create();
        alg.Mode = CipherMode.CBC;
        var key = alg.Key;
        var iv = alg.IV;

        byte[] cipher;
        using (var mem = new MemoryStream())
        {
            using(var crypt = new CryptoStream(mem, alg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (var writer = new StreamWriter(crypt))
                {
                    writer.WriteLine(loveLetter);
                }
            }
            cipher = mem.ToArray();
        }


        // Ontvanger
        Aes alg2 = Aes.Create();
        alg2.Mode =CipherMode.CBC;
        alg2.Key = key;
        alg2.IV = iv;
        using(var mem2 = new MemoryStream(cipher))
        {
            using(var crypt2 = new CryptoStream(mem2, alg2.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (var rdr = new StreamReader(crypt2))
                {
                    var data = rdr.ReadToEnd();
                    System.Console.WriteLine(data);
                }
            }
        }
    }
}
