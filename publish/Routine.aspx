<%@ Page Title="Routine Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Routine.aspx.cs" Inherits="fitnessProject.Routine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Your Routine</h2>

    <div class="diet-grid">
        <asp:Repeater ID="rptRoutine" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <div class="meal-card">
                    <img src='<%# Eval("diet_image") %>' alt="Meal Image" class="meal-img" />
                    <div class="meal-info">
                        <h3><%# Eval("mealName") %></h3>
                        <p><strong>Macros:</strong> <%# Eval("macronutrient") %></p>
                        <p><strong>Calories:</strong> <%# Eval("calories") %> kcal</p>
                        <asp:Button ID="BtnDelete" runat="server" Text="Delete from Routine"
                            CssClass="add-button"
                            CommandName="DeleteMeal" CommandArgument='<%# Eval("routine_id") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
