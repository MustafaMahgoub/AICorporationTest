using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

public partial class FirstPage : System.Web.UI.Page
{
    private string m_szFirstVariable;
    private string m_szSecondVariable;
    private string m_szThirdVariable;
    private string m_szForthVariable;
    private string m_szFifthVariable;
    private string m_szSixthVariable;
    private string m_szSeventhVariable;
    private string m_szEighthVariable;

    protected void Page_Load(object sender, EventArgs e)
    {
        Random rRandom;
        rRandom = new Random();
        // populate the variables
        m_szFirstVariable = PopulateVariable(FirstVariableLabel, ModifiedFirstVariable, rRandom);
        m_szSecondVariable = PopulateVariable(SecondVariableLabel, ModifiedSecondVariable, rRandom);
        m_szThirdVariable = PopulateVariable(ThirdVariableLabel, ModifiedThirdVariable, rRandom);
        m_szForthVariable = PopulateVariable(ForthVariableLabel, ModifiedForthVariable, rRandom);
        m_szFifthVariable = PopulateVariable(FifthVariableLabel, ModifiedFifthVariable, rRandom);
        m_szSixthVariable = PopulateVariable(SixthVariableLabel, ModifiedSixthVariable, rRandom);
        m_szSeventhVariable = PopulateVariable(SeventhVariableLabel, ModifiedSeventhVariable, rRandom);
        m_szEighthVariable = PopulateVariable(EighthVariableLabel, ModifiedEighthVariable, rRandom);

        //-----------------------------------------------------------------------------------------------------------+
        //Prep code was used to create the public and the private keys                                           |
              //var rsa = new RSAEncryption();                                                                       |
              //rsa.AssignNewKey(Server.MapPath("~/Keys/publickey.xml"), Server.MapPath("~/Keys/privatekey.xml"));   |
        //-----------------------------------------------------------------------------------------------------------+


        // Step 1 -Create 32 byte session key
        var sessionKey = KeyGenerator.GenerateKey();

        // Step 2 -Create 16 byte Initialisation Vector
        var iv = KeyGenerator.GenerateKey(16);

        // Step 3 -Encrypt the data using the session key and the IV.
        var data = Utils.BuildString(m_szFirstVariable, m_szSecondVariable, m_szThirdVariable, m_szForthVariable, m_szFifthVariable, m_szSixthVariable, m_szSeventhVariable, m_szEighthVariable); 
        AESEncryption aes = new AESEncryption();
        var encryptedData = aes.Enrypt(Encoding.UTF8.GetBytes(data), sessionKey, iv);

        // Step 4 -Hash the encrypted data using the session key
        byte[] hashedData = HashGenerator.ComputeHmacSha256(encryptedData, sessionKey);
        
        // Step 5 -Encrypt the session Key using the receiver's public key(created and stored safely before, in real life scenario will two different servers).
        RSAEncryption rsa = new RSAEncryption();        
        var encryptedSessionKey = rsa.Encrypt(Server.MapPath("~/Keys/publickey.xml"), sessionKey); 

        // Step 6 -Setting the values that will be stored in the hidden values.
        hdEncryptedSessionKey.Value = Convert.ToBase64String(encryptedSessionKey);
        hdEncryptedData.Value = Convert.ToBase64String(encryptedData);
        hdIv.Value = Convert.ToBase64String(iv); // IV Doesn't need to be encrypted.
        hdHashedData.Value = Convert.ToBase64String(hashedData);
    }

    private string PopulateVariable(Label lLabel, TextBox tbModiferTextBox, Random rRandom)
    {
        int nMode;
        string szValue = "UNASSIGNED";
        int nLength;
        int nLength2;

        nMode = rRandom.Next(3);

        switch (nMode)
        {
            // just today's date
            case 0:
                nLength = rRandom.Next(365);
                nLength2 = rRandom.Next(86400);
                szValue = DateTime.Now.AddDays(nLength * -1).AddSeconds(nLength2 * -1).ToString("dd/MM/yyyy HH:mm:ss");
                break;
            // a random number
            case 1:
                nLength = rRandom.Next(6) + 1;
                szValue = GenerateRandomNumberString(rRandom, nLength);
                break;
            // a random string
            case 2:
                nLength = rRandom.Next(12) + 1;
                szValue = GenerateRandomString(rRandom, nLength, true);
                break;
        }
        lLabel.Text = String.Format("{0} <em>(actual)</em>", szValue);
        tbModiferTextBox.Text = szValue;

        return (szValue);
    }

    public string GenerateRandomNumberString(Random rRandom, int nSize)
    {
        StringBuilder builder = new StringBuilder();
        int nInt;
        int nCount;

        for (nCount = 0; nCount < nSize; nCount++)
        {
            nInt = rRandom.Next(10);
            builder.Append(nInt);
        }

        return builder.ToString();
    }
    public string GenerateRandomString(Random random, int size, bool lowerCase)
    {
        return (GenerateRandomString(random, size, lowerCase, false));
    }
    public string GenerateRandomString(Random random, int size, bool lowerCase, bool boNumbers)
    {
        StringBuilder builder = new StringBuilder();
        char ch;
        int nNumberOrChar;

        for (int i = 0; i < size; i++)
        {
            if (!boNumbers)
            {
                ch = Convert.ToChar(random.Next(26) + 65);
            }
            else
            {
                nNumberOrChar = random.Next(2);

                if (nNumberOrChar == 0)
                {
                    ch = Convert.ToChar(random.Next(26) + 65);
                }
                else
                {
                    ch = Convert.ToChar(random.Next(10) + 48);
                }
            }
            builder.Append(ch);
        }
        if (lowerCase)
        {
            return builder.ToString().ToLower();
        }
        return builder.ToString();
    }
}