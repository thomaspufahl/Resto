<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="RestoWebClient._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resto | 404</title>
    <script src="https://cdn.tailwindcss.com"></script>
</head>
<body>
    <form id="form1" runat="server">
        <section class="flex flex-col text-left mt-36">
            <div class="self-center flex flex-col gap-y-3">
                <h1 class="mb-4 text-4xl font-extrabold leading-none tracking-tight text-gray-900 md:text-5xl lg:text-6xl dark:text-slate-200">Nothing here :(</h1>
                <h2 class="mb-4 text-4xl font-extrabold leading-none tracking-tight text-gray-900 md:text-5xl lg:text-6xl dark:text-slate-200"><mark class="px-2 text-white bg-blue-600 rounded dark:bg-blue-500">404</mark> page not found</h2>
                <a href="/" class="flex w-fit rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Go Home</a>
            </div>
        </section>
    </form>
</body>
</html>
