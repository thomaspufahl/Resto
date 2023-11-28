<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestoWebClient.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="flex flex-wrap justify-center mt-32 mx-36 gap-56 gap-y-32 text-slate-300">
        <asp:Button runat="server" ID="BtnEmployees"
            CssClass="w-96 h-36 bg-blue-700 rounded-lg text-3xl font-bold hover:bg-blue-800 shadow-md ease-in-out transition-all hover:scale-110"
            Text="Employees"
            OnClick="Employee_Click" />
        <asp:Button runat="server" ID="BtnProducts"
            CssClass="w-96 h-36 bg-blue-700 rounded-lg text-3xl font-bold hover:bg-blue-800 shadow-md ease-in-out transition-all hover:scale-110"
            Text="Products"
            OnClick="Products_Click" />
        <asp:Button runat="server" ID="BtnOrders"
            CssClass="w-96 h-36 bg-blue-700 rounded-lg text-3xl font-bold hover:bg-blue-800 shadow-md ease-in-out transition-all hover:scale-110"
            Text="Orders"
            OnClick="Orders_Click" />
        <asp:Button runat="server" ID="BtnReports"
            CssClass="w-96 h-36 bg-blue-700 rounded-lg text-3xl font-bold hover:bg-blue-800 shadow-md ease-in-out transition-all hover:scale-110"
            Text="Reports"
            OnClick="Reports_Click" />
    </section>
</asp:Content>
