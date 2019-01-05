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
        // Building the string from the incoming request
        var data = Utils.BuildString(
            Request.Form["ModifiedFirstVariable"], 
            Request.Form["ModifiedSecondVariable"], 
            Request.Form["ModifiedThirdVariable"], 
            Request.Form["ModifiedForthVariable"], 
            Request.Form["ModifiedFifthVariable"], 
            Request.Form["ModifiedSixthVariable"], 
            Request.Form["ModifiedSeventhVariable"], 
            Request.Form["ModifiedEighthVariable"]);

        // Fetch the hash from the incoming request
        var originalHash = Request.Form["hdHash"];

        // Get Triple DES seceret key and Intitial Victor
        var tripleDesKey = Utils.GetTripleDesKey();
        var tripleDesIv = Utils.GetTripleDesIv();

        //Encrypt the data recieved from the incoming request 
        var encryptedData =TripleDESEncryption.Enrypt(Encoding.UTF8.GetBytes(data), tripleDesKey, tripleDesIv);

        // Get the HMAC key to hash the encrypted data
        var hmacKey = Utils.GetHMACKey();

        // Calculate the hash for the encrypted data
        var calculatedHash = Convert.ToBase64String(HashGenerator.ComputeHmacSha256(encryptedData, hmacKey));        

        // Finally compare the hash values
        return originalHash == calculatedHash;        
    }
}