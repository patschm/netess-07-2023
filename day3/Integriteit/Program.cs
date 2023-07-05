using System.Security.Cryptography;
using System.Text;

namespace Integriteit;
class Program
{
    static void Main(string[] args)
    {
        //TestHash();
        //TestSymmetric();
        TestAsymmetric();
        Console.ReadLine();
    }

    private static void TestAsymmetric()
    {
         // Zender
        var keygen = DSA.Create();
        // Certificate
        var publicKey = keygen.ToXmlString(false);
        var privateKey = keygen.ToXmlString(true);


        string loveLetter = "Hello World";
        SHA1 alg = SHA1.Create();
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(loveLetter));
        var dsa = DSA.Create();
        dsa.FromXmlString(privateKey);
        
        byte[] signature = dsa.SignData(hash, HashAlgorithmName.SHA1);

        // Ontvanger
        SHA1 alg2 = SHA1.Create();
        byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(loveLetter));
        var dsa2 = DSA.Create();
        dsa2.FromXmlString(publicKey);
        bool isOk = dsa2.VerifyData(hash2, signature, HashAlgorithmName.SHA1);
        System.Console.WriteLine(isOk ? "Het is veilig" : "Er is aan gerommeld");
    }

    private static void TestSymmetric()
    {
         // Zender
        string loveLetter = "Hello World";
        HMACMD5 alg = new HMACMD5();
        //var key = alg.Key;
        alg.Key = Encoding.UTF8.GetBytes("Pa$$w0rd");
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(loveLetter));

        // Ontvanger
        HMACMD5 alg2 = new HMACMD5();
        alg2.Key = Encoding.UTF8.GetBytes("Pa$$w0rd");
        byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(loveLetter));
        System.Console.WriteLine(Convert.ToBase64String(hash2)); 
        System.Console.WriteLine(Convert.ToBase64String(hash));
    }

    private static void TestHash()
    {
        // Zender
        string loveLetter = "Hello World";
        MD5 alg = MD5.Create();
        byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(loveLetter));

        // Ontvanger
        MD5 alg2 = MD5.Create();
        byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(loveLetter));
        System.Console.WriteLine(Convert.ToBase64String(hash2)); 
        System.Console.WriteLine(Convert.ToBase64String(hash));

    }
}
