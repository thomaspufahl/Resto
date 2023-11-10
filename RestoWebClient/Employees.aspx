<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="RestoWebClient.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="grid gap-10 mx-auto sm:grid-cols-2 lg:grid-cols-4 lg:max-w-screen-lg mt-10">
        <asp:Repeater runat="server" ID="EmployeeList">
            <ItemTemplate>
                <div class="flex flex-col items-center p-4">
                    <img class="object-cover w-24 h-24 rounded-full shadow" style="filter:invert(100%)" src="https://imgs.search.brave.com/gHiiofE2tuuLBNW8GHiJQVHzDD8jrTZnpxJiToZui40/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9hc3Nl/dHMuc3RpY2twbmcu/Y29tL2ltYWdlcy81/ODVlNGJlYWNiMTFi/MjI3NDkxYzMzOTku/cG5n" alt="Person" />
                    <div class="flex flex-col justify-center mt-2 text-center">
                        <p class="text-lg font-bold text-white"><%# Eval("FirstName") %> <%# Eval("LastName") %></p>
                        <p class="mb-4 text-xs text-white"><%# Eval("EmployeeNumber") %></p>                    
                        <p runat="server" id="pRoleId" class="text-sm leading-6 text-white"><%# RestoWebClient.RoleConverter.Convert((int) Eval("RoleId")).ToUpper() %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
