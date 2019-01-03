<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecondPage.aspx.cs" Inherits="SecondPage" EnableEventValidation="false" EnableViewState="false" EnableViewStateMac="false" EnableSessionState="False" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" action="SecondPage.aspx">
        <div style="width:800px;margin:auto">
            <div class="ContentRight">
                <asp:Panel ID="pnMessageBox" runat="server" CssClass="WarningMessage">
                    <asp:Label ID="lbMessage" Text="YOU MUST COMPLETE THE TASK" runat="server" />
                </asp:Panel>
                <div class="ContentHeader">
                    Programmer Test: Securing Form Post Variables - RESULTS
                </div>        
                <div class="ContentBodyText">
                    Below are the variables that have been received. Can we detect the integrity of them?
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>First Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="FirstVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Second Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="SecondVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Third Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="ThirdVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Forth Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="ForthVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Fifth Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="FifthVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Sixth Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="SixthVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Seventh Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="SeventhVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="FormItem">
                    <div class="FormLabel">
                        <strong>Eighth Variable: </strong>
                    </div>
                    <div class="FormInputTextOnly">
                        <asp:Label ID="EighthVariableLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
