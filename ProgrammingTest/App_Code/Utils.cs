using System.Security.Cryptography;
using System.Configuration;
using System.Text;
using System;

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
    public static byte[] GetDesKey()
    {
        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Convert.FromBase64String(ConfigurationManager.AppSettings["DesKey"].ToString());
    }
    public static byte[] GetDesIv()
    {
        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Convert.FromBase64String(ConfigurationManager.AppSettings["DesIv"].ToString());
    }
    public static byte[] GetTripleDesKey()
    {
        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Convert.FromBase64String(ConfigurationManager.AppSettings["TripleDesKey"].ToString());
    }
    public static byte[] GetTripleDesIv()
    {
        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Convert.FromBase64String(ConfigurationManager.AppSettings["TripleDesIv"].ToString());
    }    
    public static byte[] GetAesKey()
    {
        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Convert.FromBase64String(ConfigurationManager.AppSettings["AesKey"].ToString());
    }
    public static byte[] GetAesIv()
    {
        // The key must be shared between the client and the server.
        // The key will be givin the client on some secure way.
        // The key must be stored in secure location.
        // The key will never be sent on the request on the response.
        return Convert.FromBase64String(ConfigurationManager.AppSettings["AesIv"].ToString());
    }
    public static string BuildString(
                        string m_szFirstVariable,
                        string m_szSecondVariable,
                        string m_szThirdVariable,
                        string m_szForthVariable,
                        string m_szFifthVariable,
                        string m_szSixthVariable,
                        string m_szSeventhVariable,
                        string m_szEighthVariable)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(m_szFirstVariable);
        builder.Append(m_szSecondVariable);
        builder.Append(m_szThirdVariable);
        builder.Append(m_szForthVariable);
        builder.Append(m_szFifthVariable);
        builder.Append(m_szSixthVariable);
        builder.Append(m_szSeventhVariable);
        builder.Append(m_szEighthVariable);
        return builder.ToString();
    }
}
