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
    private bool ValidateIncomingVariables(HttpRequest request)
    {
        bool isGenuine = false;

        // Step 1 -Read the follwoing from the hidden values
            //1 -Request Encrypted Session Key
            //2- Request Encrypted Data 
            //3- Request IV 
            //4- Request Encrypted Data`s Hash
        var requestEncryptedSessionKeyBytes = Convert.FromBase64String(Request.Form["hdEncryptedSessionKey"]);
        var requestEncryptedDataBytes = Convert.FromBase64String(Request.Form["hdEncryptedData"]);
        var requestIvBytes = Convert.FromBase64String(Request.Form["hdIv"]);
        var requestHashedDataBytes = Convert.FromBase64String(Request.Form["hdHashedData"]);

        // Step 2 -Decrypt the request session key using the receiver's private key.        
        var rsa = new RSAEncryption();
        var requestDecryptedSessionKey = rsa.Decrypt(Server.MapPath("~/Keys/privatekey.xml"), requestEncryptedSessionKeyBytes);        
        
        // Step 3-Building the string from the incoming request's actual data
        var actualData = Utils.BuildString(
            Request.Form["ModifiedFirstVariable"],
            Request.Form["ModifiedSecondVariable"],
            Request.Form["ModifiedThirdVariable"],
            Request.Form["ModifiedForthVariable"],
            Request.Form["ModifiedFifthVariable"],
            Request.Form["ModifiedSixthVariable"],
            Request.Form["ModifiedSeventhVariable"],
            Request.Form["ModifiedEighthVariable"]);

        // Step 4-Encrypt the incoming request's actual data using the decrypted session key and the iv.
        AESEncryption aes = new AESEncryption();
        var actualEncryptedData = aes.Enrypt(Encoding.UTF8.GetBytes(actualData), requestDecryptedSessionKey, requestIvBytes);
        var actualDataHash = HashGenerator.ComputeHmacSha256((actualEncryptedData), requestDecryptedSessionKey);
        
        // Step 5-Compare the actual hash with received hash(from request)
        var isActualDataHashMatch = Compare(requestHashedDataBytes, actualDataHash);
        
        // Step 6-Calculate the hash for the encrypted data 
        var requestEncryptedDataHash = HashGenerator.ComputeHmacSha256((requestEncryptedDataBytes), requestDecryptedSessionKey);
        var isRequestEncryptedDataHashMatch = Compare(actualDataHash, requestEncryptedDataHash);
        
        if (isActualDataHashMatch && isRequestEncryptedDataHashMatch)
        {
            //Step 7 -Decrypt the data using the decrypted session key and the iv. - out of scope                       
            //var decryptedDataBytes = aes.Decrypt(encryptedDataBytes, decryptedSessionKey, ivBytes);
            //var plainData =Encoding.Default.GetString(decryptedDataBytes);
            isGenuine= true;
        }        
        return isGenuine;
    }
    private bool Compare(byte[] array1, byte[] array2)
    {
        var result = array1.Length == array2.Length;
        for (int i = 0; i < array1.Length && i < array2.Length; ++i)
        {
            bool isEqual = (array1[i] == array2[i]);
            result &= isEqual;
        }
        return result;
    }
}