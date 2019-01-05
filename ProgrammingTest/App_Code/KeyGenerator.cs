using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

public static class HMACKeyGenerator
{
    private const int KeySize = 32;

    public static byte[] GenerateKey()
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[KeySize];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }

    }
}