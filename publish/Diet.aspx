<%@ Page Title="Diet Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Diet.aspx.cs" Inherits="fitnessProject.Diet" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Your Diet Meals</h2>

    <div class="diet-grid">
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="RptDiets_ItemCommand">
            <ItemTemplate>
    <div class="meal-card">
        <img src='<%# Eval("diet_image") %>' alt="Meal Image" class="meal-img" />
        <div class="meal-info">
            <h3><%# Eval("mealName") %></h3>
            <p><strong>Macros:</strong> <%# Eval("macronutrient") %></p>
            <p><strong>Calories:</strong> <%# Eval("calories") %> kcal</p>

            <% if (Session["Role"] != null && Session["Role"].ToString() == "admin") { %>
                <asp:Button ID="btnEdit" runat="server" Text="Edit Diet"
                    CssClass="edit-button"
                    CommandName="Edit" CommandArgument='<%# Eval("diet_id") %>' />

                <asp:Button ID="btnDelete" runat="server" Text="Delete Diet"
                    CssClass="delete-button"
                    CommandName="Delete" CommandArgument='<%# Eval("diet_id") %>' />

                <!-- Inline Edit Form -->
                <asp:Panel ID="editPanel" runat="server" Visible="false" CssClass="edit-form">
                    <label>Meal Name:</label>
                    <asp:TextBox ID="txtMealName" runat="server" Text='<%# Eval("mealName") %>' CssClass="form-control" />

                    <label>Macros:</label>
                    <asp:TextBox ID="txtMacros" runat="server" Text='<%# Eval("macronutrient") %>' CssClass="form-control" />

                    <label>Calories:</label>
                    <asp:TextBox ID="txtCalories" runat="server" Text='<%# Eval("calories") %>' CssClass="form-control" />

                    <label>Image URL:</label>
                    <asp:TextBox ID="txtImage" runat="server" Text='<%# Eval("diet_image") %>' CssClass="form-control" />

                    <asp:Button ID="btnSave" runat="server" Text="Save Changes"
                        CommandName="Save" CommandArgument='<%# Eval("diet_id") %>' CssClass="save-button" />
                </asp:Panel>

            <% } else { %>
                <asp:Button ID="btnAdd" runat="server" Text="Add to Routine"
                    CssClass="add-button"
                    CommandName="Add" CommandArgument='<%# Eval("diet_id") %>' />
            <% } %>
        </div>
    </div>
</ItemTemplate>

        </asp:Repeater>

    </div>

    <div id="successMessage" runat="server" clientidmode="Static" class="success-message" style="display: none;">
        Meal updated or added successfully!
    </div>

    <div id="successDeleteMessage" runat="server" clientidmode="Static" class="success-message" style="display: none;">
        Meal deleted successfully!
    </div>
</asp:Content>
