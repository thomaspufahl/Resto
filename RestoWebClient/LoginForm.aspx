<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="RestoWebClient.LoginForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-2xl italic">Login</h1>
    <div class="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
        <div class="sm:mx-auto sm:w-full sm:max-w-sm">
            <img class="mx-auto h-20 w-20 " src="https://imgs.search.brave.com/wj5qP54LsLYocYGWamTkWHtRbWG-b0BeUyhUQZ-WJrI/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9pbWFn/ZXMudmV4ZWxzLmNv/bS9tZWRpYS91c2Vy/cy8zLzEzNzA0Ny9p/c29sYXRlZC9wcmV2/aWV3LzU4MzFhMTdh/MjkwMDc3YzY0NmE0/OGM0ZGI3OGE4MWJi/LWljb25vLWRlLXBl/cmZpbC1kZS11c3Vh/cmlvLWF6dWwucG5n" alt="Your Company">
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
                </div>
            </div>
        </div>
    </div>
    <span><a href="RegisterForm.aspx" class="underline">Register</a></span>
</asp:Content>
