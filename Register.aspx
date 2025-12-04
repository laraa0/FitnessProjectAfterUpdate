<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="fitnessProject.Register" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sign Up</title>
    <link rel="stylesheet" type="text/css" href="Content/login.css" />
</head>
<body>
    <form id="registerForm" runat="server">
        <h2>Sign Up</h2>

        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label><br />
        <asp:TextBox ID="email" runat="server" TextMode="Email" CssClass="input-box" /><br /><br />

        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label><br />
        <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="input-box" /><br /><br />

        <asp:Label ID="lblConfirm" runat="server" Text="Confirm Password:"></asp:Label><br />
        <asp:TextBox ID="confirmPassword" runat="server" TextMode="Password" CssClass="input-box" /><br /><br />

        <asp:Button ID="registerButton" runat="server" Text="Sign Up" OnClick="RegisterButton_Click" CssClass="login-btn" /><br /><br />

        <p>Already have an account? <a href="Login.aspx">Login here</a></p>
    </form>
</body>
</html>
