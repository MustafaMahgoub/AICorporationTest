using System;
using System.IO;
using System.Security.Cryptography;


public class RSAEncryption
{
    private RSAParameters _publicKey;
    private RSAParameters _privateKey;


    public void AssignNewKey()
    {
        using (var rsa=new RSACryptoServiceProvider(2048)) 
        {
            rsa.PersistKeyInCsp = false;
            _publicKey = rsa.ExportParameters(false);
            _privateKey = rsa.ExportParameters(true);

        }
    }
    public void AssignNewKey(string publicKeyPath, string privateKeyPath)
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;


            if (File.Exists(privateKeyPath))
            {
                File.Delete(privateKeyPath);
            }
            if (File.Exists(publicKeyPath))
            {
                File.Delete(publicKeyPath);
            }

            var publicKeyFolder = Path.GetDirectoryName(publicKeyPath);
            var privateKeyFolder = Path.GetDirectoryName(privateKeyPath);

            if (!Directory.Exists(publicKeyFolder))
            {
                Directory.CreateDirectory(publicKeyFolder);
            }
            if (!Directory.Exists(privateKeyFolder))
            {
                Directory.CreateDirectory(privateKeyFolder);
            }

            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
        }
    }

   
    public byte[] Encrypt(string publicKeyPath,byte[] dataToenrypt)
    {
        byte[] cipherBytes;

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(File.ReadAllText(publicKeyPath));
            cipherBytes = rsa.Encrypt(dataToenrypt, false);
        }
        return cipherBytes;

    }
    public byte[] Decrypt(string privateKeyPath, byte[] dataToenrypt)
    {
        byte[] plain;

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(File.ReadAllText(privateKeyPath));
            plain = rsa.Decrypt(dataToenrypt, false);
        }
        return plain;
    }


    [Obsolete]
    public byte[] Encrypt(byte[] dataToenrypt) {

        byte[] cipherBytes;

        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_publicKey);

            cipherBytes = rsa.Encrypt(dataToenrypt,true);
        }
        return cipherBytes;
    }
    [Obsolete]
    public byte[] Decrypt(byte[] dataToDecrypt)
    {
        byte[] plain;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_privateKey);
            plain = rsa.Decrypt(dataToDecrypt, true);
        }
        return plain;
    }

}