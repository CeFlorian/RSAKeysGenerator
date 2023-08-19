// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

//url reference
//https://vcsjones.dev/key-formats-dotnet-3/



string GeneratePEM(string format, RSA rsa)
{
    string hdr = $"-----BEGIN {format}-----";
    string ftr = $"-----END {format}-----";
    string key = null;

    switch (format)
    {
        case "RSA PRIVATE KEY":
            key = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            break;
        case "PRIVATE KEY":
            key = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());
            break;
        case "RSA PUBLIC KEY":
            key = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            break;
        case "PUBLIC KEY":
            key = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
            break;
        default:
            break;
    }

    return $"{hdr}{key}{ftr}";
}



bool optExit = false;

while (!optExit)
{
    string prvPEM = null;
    string pubPEM = null;

    string pubFormat = null;
    string prvFormat = null;

    int keySize = 0;

    Console.Clear();

    Console.WriteLine(@"
RSA KEYS GENERATOR
------------------

Export options:

1 : default openssl     (RSA PRIVATE KEY - PUBLIC KEY - 4096)
2 : rsa-rsa             (RSA PRIVATE KEY - RSA PUBLIC KEY - 4096)
3 : custom              (? - ? - ?)
0 : exit

Enter option number: ");


    string option = Console.ReadLine();

    switch (option)
    {
        case "1":
            prvFormat = "RSA PRIVATE KEY";
            pubFormat = "PUBLIC KEY";
            keySize = 4096;
            break;
        case "2":
            prvFormat = "RSA PRIVATE KEY";
            pubFormat = "RSA PUBLIC KEY";
            keySize = 4096;
            break;
        case "3":
            Console.WriteLine("The selected option is not yet available");
            Console.ReadKey();
            continue;
        case "0":
            optExit = true;
            continue;
        default:
            Console.WriteLine("The selected option is not valid");
            Console.ReadKey();
            continue;
    }


    RSA rsa = RSA.Create();
    rsa.KeySize = keySize;

    prvPEM = GeneratePEM(prvFormat, rsa);
    pubPEM = GeneratePEM(pubFormat, rsa);

    Console.WriteLine($@"
Generated keys: 
({prvFormat} - {pubFormat} - {keySize})

Private PEM:

{prvPEM}

Public PEM:

{pubPEM}
");

    Console.ReadKey();
}



