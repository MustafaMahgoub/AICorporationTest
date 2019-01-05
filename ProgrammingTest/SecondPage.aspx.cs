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
        var data =
            Request.Form["ModifiedFirstVariable"] +
            Request.Form["ModifiedSecondVariable"] +
            Request.Form["ModifiedThirdVariable"] +
            Request.Form["ModifiedForthVariable"] +
            Request.Form["ModifiedFifthVariable"] +
            Request.Form["ModifiedSixthVariable"] +
            Request.Form["ModifiedSeventhVariable"] +
            Request.Form["ModifiedEighthVariable"];
        

        var originalHash = Request.Form["hdHash"];
        var hmacKey = Utils.GetHMACKey();
        var calculatedHash = Convert.ToBase64String(Utils.ComputeHmacSha256(Encoding.UTF8.GetBytes(data), hmacKey));

        Utils.DecryptMessage(data);
        return originalHash == calculatedHash;

        //var calculatedHash = HashGenerator.GenerateMd5Hash(data);
        //return originalHash == calculatedHash;

    }
}