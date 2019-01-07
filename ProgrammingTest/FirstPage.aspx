<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FirstPage.aspx.cs" Inherits="FirstPage" EnableEventValidation="false" EnableViewState="false" EnableViewStateMac="false" EnableSessionState="False" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" action="SecondPage.aspx">
        <asp:HiddenField runat="server" ID="hdEncryptedSessionKey"/>
        <asp:HiddenField runat="server" ID="hdEncryptedData"/>
        <asp:HiddenField runat="server" ID="hdIv"/>
        <asp:HiddenField runat="server" ID="hdHashedData"/>

        <div style="width:800px;margin:auto">
        <div class="ContentRight">
            <div class="ContentHeader">
                Programmer Test: Securing Form Post Variables
            </div>
            <div class="ContentBodyText">
                <p>
                    This test requires the securing post variables between a cross page post to SecondPage.aspx. It is important
                    to understand that whilst the values of the variables are greated on the server, they are transmitted between
                    the pages via the client's browser. We need to protect the system by detecting whether the data was tampered with
                    whilst it is in transit through the client's browser.
                </p>
                <p>
                    The variables are given below - the text boxes next to the actual value gives a method to simulate the data
                    being tampered with.
                </p>
                <p>
                    Please could you implement a method for validating the data integrity when it is posted from FirstPage.aspx to
                    SecondPage.aspx. If the data is tampered with, SecondPage.aspx should detect this and display an error message
                </p>
                <p>
                    <strong>IMPORTANT: </strong>Passing crieria:
                </p>
                <ul>
                    <li>Starting on FirstPage.aspx, if the POST! button is clicked when no variables have been modified, the validation on
                        SecondPage.aspx should detect no tampering and display an appropriate message</li>
                    <li>Starting on FirstPage.aspx, if one or more variable values are modified before the POST! button is clicked, the validation on
                        SecondPage.aspx should detect tampering and display an appropriate message</li>
                </ul>
                <p>
                    <strong>IMPORTANT: </strong>Restrictions of the task:
                </p>
                <ul>
                    <li>You must start on FirstPage.aspx and click the POST! button to navigate to SecondPage.aspx - no other navigation is allowed</li>
                    <li>No session state or server side storage can be used</li>
                    <li>Whilst FirstPage.aspx and SecondPage.aspx can share code and configuration, you CANNOT use any structures that rely on FirstPage.aspx and SecondPage.aspx being in the same app - e.g. PreviousPage</li>
                    <li>Apart from class files (.cs), no extra files (e.g. pages, controls, web services etc can be added to the project)</li>
                    <li>The viewstate for both pages must remain OFF</li>
                    <li>As the textboxes are intended to simulate the modification of the data, SecondPage.aspx MUST only reference the incoming textboxes - you CANNOT reference the data in the labels (either directly or by scraping/parsing the HTML)</li>
                    <li>You may add additional fields to the form post - but these fields CANNOT contain the initial variable values in an unaltered form</li>
                    <li>You do not have to detect which variable was tampered with - only that tampering has occurred</li>
                    <li>You may add additional configuration items to the web.config file that can be referenced by both FirstPage.aspx and SecondPage.aspx</li>
                </ul>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>First Variable: </strong>
                    <asp:Label ID="FirstVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedFirstVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Second Variable: </strong>
                    <asp:Label ID="SecondVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedSecondVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Third Variable: </strong>
                    <asp:Label ID="ThirdVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedThirdVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Forth Variable: </strong>
                    <asp:Label ID="ForthVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedForthVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Fifth Variable: </strong>
                    <asp:Label ID="FifthVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedFifthVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Sixth Variable: </strong>
                    <asp:Label ID="SixthVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedSixthVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Seventh Variable: </strong>
                    <asp:Label ID="SeventhVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedSeventhVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormLabel">
                    <strong>Eighth Variable: </strong>
                    <asp:Label ID="EighthVariableLabel" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="FormInput">
                    &nbsp;MODIFY HERE&nbsp;&nbsp;-----&gt; 
                    <asp:TextBox ID="ModifiedEighthVariable" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="FormItem">
                <div class="FormSubmit">
                    <asp:Button ID="Button1" runat="server" Text="POST!" />
                </div>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
