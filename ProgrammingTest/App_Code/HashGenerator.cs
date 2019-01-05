using System;
using System.Security.Cryptography;
using System.Text;

public class HashGenerator
{   
    public static byte[] ComputeHmacSha256(byte[] toBeHashed, byte[] key)
    {
        using (var hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(toBeHashed);
        }
    }
    [Obsolete]
    public static string GenerateMd5Hash(string data)
    {
        byte[] buffer;
        byte[] binaryHash;
        MD5 md5;
        md5 = MD5.Create();

        // Even if the middleman was able to change the data and re-generate the hash, we would be able to detect that because the he does not know the Prefix&Suffix.
        // If the two servers are in sync, we could use date/time.(ie Salt)
        var prefix = System.Configuration.ConfigurationManager.AppSettings["Prefix"].ToString();
        var suffix = System.Configuration.ConfigurationManager.AppSettings["Suffix"].ToString();

        buffer = Encoding.Unicode.GetBytes(prefix + data + suffix);

        // Hash it- there are other algorithms.
        binaryHash = md5.ComputeHash(buffer);
        return Convert.ToBase64String(binaryHash);
    }
}