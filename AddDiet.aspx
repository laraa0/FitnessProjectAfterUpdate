<%@ Page Title="Add Diet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDiet.aspx.cs" Inherits="fitnessProject.AddDiet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h2 class="form-title">Add New Diet</h2>

        <div class="form-group">
            <asp:Label ID="lblDietImage" runat="server" Text="Diet Image URL:" AssociatedControlID="dietImage" CssClass="form-label" />
            <asp:TextBox ID="dietImage" runat="server" CssClass="form-input" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblMealName" runat="server" Text="Meal Name:" AssociatedControlID="mealName" CssClass="form-label" />
            <asp:TextBox ID="mealName" runat="server" CssClass="form-input" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblMacronutrient" runat="server" Text="Macronutrient:" AssociatedControlID="macronutrient" CssClass="form-label" />
            <asp:TextBox ID="macronutrient" runat="server" CssClass="form-input" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblCalories" runat="server" Text="Calories:" AssociatedControlID="calories" CssClass="form-label" />
            <asp:TextBox ID="calories" runat="server" CssClass="form-input" />
        </div>

        <asp:Button ID="btnAddDiet" runat="server" Text="Add Diet" OnClick="AddDietButton_Click" CssClass="add-button" />

        <div class="form-footer">
            <a class="back-link" href="Diet.aspx">← Back to Diets</a>
        </div>
    </div>
</asp:Content>
