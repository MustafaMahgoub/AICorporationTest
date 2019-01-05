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

        //---------------------------------------------------------+
        // Level 4 security - USING TRIPLE DES ENCRYPTION (3 Keys) |
        //---------------------------------------------------------+

        //-----------------------------------------------------------+
        // USED ONCE TO GENERATE THE KEYS AND STORE IT IN THE CONFIG |
            //var keyString= Convert.ToBase64String(KeyGenerator.GenerateKey(24));
            //var ivString = Convert.ToBase64String(KeyGenerator.GenerateKey(8));
        //-----------------------------------------------------------+

        var data = Utils.BuildString(m_szFirstVariable, m_szSecondVariable, m_szThirdVariable, m_szForthVariable, m_szFifthVariable, m_szSixthVariable, m_szSeventhVariable, m_szEighthVariable);
        var desKey = Utils.GetTripleDesKey();
        var desIv = Utils.GetTripleDesIv();
        var encryptedData = TripleDESEncryption.Enrypt(Encoding.UTF8.GetBytes(data), desKey, desIv);

        var hmacKey = Utils.GetHMACKey();
        byte[] hashedValue = HashGenerator.ComputeHmacSha256(encryptedData, hmacKey);

        hdHash.Value = Convert.ToBase64String(hashedValue);        
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