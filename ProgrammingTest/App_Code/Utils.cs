using System.Security.Cryptography;
using System.Configuration;
using System.Text;

public static class Utils
{
    public static byte[] GetHMACKey()
    {

        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["HMACKey"].ToString());
    }
    public static byte[] ComputeHmacSha256(byte[] toBeHashed, byte[] key)
    {

        using (var hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(toBeHashed);
        }
    }
    public static string EnryptMessage(string msg)
    {
        // TODO
        return msg;
    }
    public static string DecryptMessage(string msg)
    {

        // TODO
        return msg;
    }
}
