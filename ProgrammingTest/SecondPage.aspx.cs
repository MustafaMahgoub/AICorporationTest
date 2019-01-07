using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class SecondPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FirstVariableLabel.Text = Request.Form["ModifiedFirstVariable"];
        SecondVariableLabel.Text = Request.Form["ModifiedSecondVariable"];
        ThirdVariableLabel.Text = Request.Form["ModifiedThirdVariable"];
        ForthVariableLabel.Text = Request.Form["ModifiedForthVariable"];
        FifthVariableLabel.Text = Request.Form["ModifiedFifthVariable"];
        SixthVariableLabel.Text = Request.Form["ModifiedSixthVariable"];
        SeventhVariableLabel.Text = Request.Form["ModifiedSeventhVariable"];
        EighthVariableLabel.Text = Request.Form["ModifiedEighthVariable"];

        try
        {
            if (ValidateIncomingVariables(Request))
            {
                pnMessageBox.CssClass = "SuccessMessage";
                lbMessage.Text = "Variables Validated!";
            }
            else
            {
                pnMessageBox.CssClass = "ErrorMessage";
                lbMessage.Text = "Variables Tampering Detected!";
            }
        }
        catch (Exception exc)
        {
            pnMessageBox.CssClass = "WarningMessage";
            lbMessage.Text = exc.Message;
        }
    }

    private bool Compare(byte[] array1, byte[] array2)
    {

        var result = array1.Length == array2.Length;

        for (int i = 0; i < array1.Length && i < array2.Length; ++i)
        {
            result &= array1[i] == array2[i];
        }
        return result;
    }
    private bool ValidateIncomingVariables(HttpRequest request)
    {
        // Step 1 -Read the follwoing from the hidden values
            //1 -Encrypted Session Key
            //2- Encrypted Data 
            //3- IV 
            //4- Encrypted Data`s Hash
        var encryptedSessionKeyBytes = Convert.FromBase64String(Request.Form["hdEncryptedSessionKey"]);
        var encryptedDataBytes = Convert.FromBase64String(Request.Form["hdEncryptedData"]);
        var ivBytes = Convert.FromBase64String(Request.Form["hdIv"]);
        var hashedDataBytes = Convert.FromBase64String(Request.Form["hdHashedData"]);

        // Step 2 -Decrypt the session key using the receiver's private key.        
        var rsa = new RSAEncryption();
        var decryptedSessionKey = rsa.Decrypt(Server.MapPath("~/Keys/privatekey.xml"), encryptedSessionKeyBytes);        
        
        // Step 3-Building the string from the incoming request 
        var actualRequestData = Utils.BuildString(
            Request.Form["ModifiedFirstVariable"],
            Request.Form["ModifiedSecondVariable"],
            Request.Form["ModifiedThirdVariable"],
            Request.Form["ModifiedForthVariable"],
            Request.Form["ModifiedFifthVariable"],
            Request.Form["ModifiedSixthVariable"],
            Request.Form["ModifiedSeventhVariable"],
            Request.Form["ModifiedEighthVariable"]);

        // Step 4-Encrypt the incoming actual request`s data using the same decrypted session key and the iv.
        AESEncryption aes = new AESEncryption();
        var encryptedData = aes.Enrypt(Encoding.UTF8.GetBytes(actualRequestData), decryptedSessionKey, ivBytes);
        var actualRequestDataHash = HashGenerator.ComputeHmacSha256((encryptedData), decryptedSessionKey);
        
        // Step 6-Compare the actual hash with received hash
        var isMatch = Compare(hashedDataBytes, actualRequestDataHash);

        if (isMatch)
        {
            //Step 7 -Decrypt the data using the decrypted session key and the iv. - out of scope                       
            //var decryptedDataBytes = aes.Decrypt(encryptedDataBytes, decryptedSessionKey, ivBytes);
            //var plainData =Encoding.Default.GetString(decryptedDataBytes);
        }        
        return isMatch;
    }
}