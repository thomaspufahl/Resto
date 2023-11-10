﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="RestoWebClient.LoginForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-2xl italic">Login</h1>
    <div class="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
        <div class="sm:mx-auto sm:w-full sm:max-w-sm">
            <img class="mx-auto h-20 w-20 " style=" filter: invert(100%) " src="https://imgs.search.brave.com/gHiiofE2tuuLBNW8GHiJQVHzDD8jrTZnpxJiToZui40/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9hc3Nl/dHMuc3RpY2twbmcu/Y29tL2ltYWdlcy81/ODVlNGJlYWNiMTFi/MjI3NDkxYzMzOTku/cG5n" alt="Your Company">
            <h2 class="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-white">Sign in to your account</h2>
        </div>

        <div class="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
            <div class="space-y-6" action="#">
                <div>
                    <label for="employeeNumber" class="block text-sm font-medium leading-6 text-white">Employee Number</label>
                    <div class="mt-2">
                        <input id="employeeNumberInput" runat="server" name="employeeNumber" type="text" required class="block w-full rounded-md border-0 py-1.5 text-black shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6">
                    </div>
                </div>

                <div>
                    <asp:Button ID="BtnLogin" Text="Sign in" runat="server" OnClick="BtnLogin_Click" CssClass="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"/>
                    <br />
                    <asp:Button ID="BtnRegister" Text="Register" runat="server" OnClick="BtnRegister_Click" CssClass="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
