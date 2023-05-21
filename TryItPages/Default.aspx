<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW5.Default" %>
<%@ OutputCache Duration="50" VaryByParam="*" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Zipcode Search</h1>
            <p>Enter a zipcode and to lookup area information and news articles.</p>
            <asp:TextBox ID="textbox1" runat="server"></asp:TextBox>
            <asp:Button id="button1" runat="server" text="Search" OnClick="button1_Click"/>
        </div>
        
    <div id="storeDiv" runat="server">
        <asp:Label ID="label1" runat="server" Text="Location search" Visible="False" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <asp:Label ID="label2" runat="server" Text="Based on your area search, enter any location name" Visible="false"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
        <asp:Button ID="storeButton" runat="server" Text="Search" OnClick="storeButton_Click" Visible="False" />
        <br />
        <asp:TextBox ID="textbox3" runat="server" Visible="false" ReadOnly="true" TextMode="MultiLine" Width="679px" Height="134px"></asp:TextBox>
    </div>
        
        <div id="storelistDiv" runat="server">
        </div>
    <br />
    <br />
    <div id="crimeDiv" runat="server">
        <asp:Label ID="label3" runat="server" Text="Crime data" Visible="False" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <asp:Label ID="label4" runat="server" Text="Below are crime data from the past 10 years" Visible="false"></asp:Label>
        <br />
        <asp:label ID="label5" runat="server" Visible="false"></asp:label>
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:label ID="label6" runat="server" Visible="false"></asp:label>
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:label ID="label7" runat="server" Visible="false"></asp:label>
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:label ID="label8" runat="server" Visible="false"></asp:label>
        <br />&nbsp;&nbsp;&nbsp; <asp:label ID="label9" runat="server" Visible="false"></asp:label>
        <br />&nbsp;&nbsp;&nbsp; <asp:label ID="label10" runat="server" Visible="false"></asp:label>
        <br />&nbsp;&nbsp;&nbsp; <asp:label ID="label12" runat="server" Visible="false"></asp:label>
        <br />&nbsp;&nbsp;&nbsp; <asp:label ID="label13" runat="server" Visible="false"></asp:label>
        <br />&nbsp;&nbsp;&nbsp; <asp:label ID="label14" runat="server" Visible="false"></asp:label>
    </div>
    <br />
    <br />
    <div id ="newsHeader" runat="server">
        <asp:label ID="label15" runat="server" Text="Local news" Visible="False" Font-Bold="True" Font-Size="XX-Large"></asp:label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Get News" Visible="False" OnClick="Button2_Click1" />
        <br />
    </div>
        
    </form>
    <br />
    <div id="outDiv" runat="server"></div>
</body>
</html>
