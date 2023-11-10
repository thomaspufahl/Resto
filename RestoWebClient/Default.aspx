<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestoWebClient.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex items-center justify-center mt-7">
        <p runat="server" id="WelcomeMessage" class="font-bold  text-7xl text-center text-white italic"></p>
    </div>
    <div class="relative h-screen">
        <div class="grid grid-cols-2 gap-4">
            <div class="absolute top-40 left-80 bg-blue-500 text-white p-6 text-center box-border border-4 w-1/4 rounded-lg">
                <asp:Button class="py-4 px-8 text-3xl text text-gray-800 italic " Onclick="Employee_Click" Text="Empleados" runat="server" />
            </div>
            <div class="absolute top-40 right-80 bg-indigo-500 text-white p-6 text-center box-border border-4 w-1/4 rounded-lg">
                <asp:Button class="py-4 px-8 text-3xl text-gray-800 italic" Onclick="Products_Click" Text="Productos" runat="server" />
            </div>
            <div class="absolute bottom-60 left-80 bg-purple-500 text-white p-6 text-center box-border border-4 w-1/4 rounded-lg">
                <asp:Button class="py-4 px-8 text-3xl  text-gray-800 italic" Onclick="Orders_Click" Text="Pedidos" runat="server" />
            </div>
            <div class="absolute bottom-60 right-80 bg-violet-500 text-white p-6 text-center box-border border-4 w-1/4 rounded-lg">
                <asp:Button class="py-4 px-8 text-3xl text-gray-800 italic" Onclick="Reports_Click" Text="Reportes" runat="server" />

            </div>
        </div>
    </div>

</asp:Content>
