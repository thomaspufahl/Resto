<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RestoWebClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resto | Login</title>
    <script src="https://cdn.tailwindcss.com"></script>
</head>
<body class="bg-slate-100 text-gray-900 dark:bg-gray-800 dark:text-slate-100">
    <form id="form1" runat="server" class="flex w-screen h-screen justify-center content-center bg-slate-100 text-gray-900 dark:bg-gray-800 dark:text-slate-100">
        <main class="flex flex-col w-full max-w-4xl px-6 py-12 lg:px-8 text-center">
            <div class="flex items-center justify-center space-x-3 rtl:space-x-reverse">
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-tools-kitchen-2 bg-blue-700 rounded-lg" width="44" height="44" viewBox="0 0 24 24" stroke-width="1.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <path d="M19 3v12h-5c-.023 -3.681 .184 -7.406 5 -12zm0 12v6h-1v-3m-10 -14v17m-3 -17v3a3 3 0 1 0 6 0v-3" />
                </svg>
                <h1 class="top-0 mt-2 text-3xl font-bold tracking-tight text-gray-900 dark:text-white sm:text-4xl">Resto</h1>
            </div>
            <hr class="m-12" />
            <section>
                <div class="sm:mx-auto sm:w-full sm:max-w-sm">
                    <h2 class="text-center text-2xl font-bold leading-9 tracking-tight text-gray-900 dark:text-white">Sign in to your account</h2>
                </div>

                <div class="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
                    <div class="space-y-6" action="#">
                        <div>
                            <label for="employeeNumber" class="block text-sm font-medium leading-6 text-gray-900 dark:text-white text-left">Employee Number</label>
                            <div class="mt-2">
                                <input id="employeeNumberInput" runat="server" name="employeeNumber" type="text" required="required" class="block w-full rounded-md border-0 py-1.5 px-2 text-black shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                            </div>
                        </div>

                        <div>
                            <asp:Button ID="BtnLogin" Text="Sign in" runat="server" OnClick="BtnLogin_Click" CssClass="flex w-full justify-center rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600" />
                        </div>
                    </div>
                </div>
            </section>
        </main>
    </form>
</body>
</html>
