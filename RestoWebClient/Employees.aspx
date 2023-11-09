<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="RestoWebClient.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Employees</h2>
    <a href="EmployeeForm.aspx">employee form</a>
    <ul role="list" class="divide-y divide-gray-100">
        <asp:Repeater runat="server" ID="EmployeeList">
            <ItemTemplate>
                <li class="flex justify-between gap-x-6 py-5">
                    <div class="flex min-w-0 gap-x-4">
                        <img class="h-12 w-12 flex-none rounded-full bg-gray-50" src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="">
                        <div class="min-w-0 flex-auto">
                            <p class="text-sm font-semibold leading-6 text-gray-900"><%# Eval("FirstName") %> <%# Eval("LastName") %></p>
                            <p class="mt-1 truncate text-xs leading-5 text-gray-500"><%# Eval("EmployeeNumber") %></p>
                        </div>
                    </div>
                    <div class="hidden shrink-0 sm:flex sm:flex-col sm:items-end">
                        <p runat="server" id="pRoleId" class="text-sm leading-6 text-gray-900"><%# RestoWebClient.RoleConverter.Convert((int)(Eval("RoleId"))).ToUpperInvariant() %></p>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>