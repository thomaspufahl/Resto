<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestoWebClient.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex items-center justify-center h-96" >
        <p runat="server" id="WelcomeMessage" class="font-bold  text-7xl text-center text-white italic"></p>
    </div>

</asp:Content>
