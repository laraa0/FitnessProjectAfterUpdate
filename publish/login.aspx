<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="fitnessProject.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="Content/login.css" />
</head>
<body>
    <form id="loginForm" runat="server">
        <h2>Login</h2>

        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label><br />
        <asp:TextBox ID="email" runat="server" TextMode="Email" CssClass="input-box" /><br /><br />

        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label><br />
        <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="input-box" /><br /><br />

        <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="LoginButton_Click" CssClass="login-btn" />

        <p>Don't have an account? <a href="Register.aspx">Sign up here</a></p>
    </form>
</body>
</html>
