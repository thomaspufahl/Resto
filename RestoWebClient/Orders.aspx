<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RestoWebClient.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="flex mx-auto mt-2 gap-4 flex-wrap">
        <header class="flex flex-row justify-center bg-white p-5 rounded w-full">
            header
        </header>
        <div class="flex justify-center bg-white p-5 rounded w-full gap-2">
            <aside class="flex flex-col justify-center bg-lime-200 p-5 rounded w-full max-w-md">
                <div>
                    <h2>Table: 2</h2>
                    <h2>Order Number: 111111</h2>
                    <hr class="m-2" />
                    <ol>
                        <li>Order item 1</li>
                        <li>Order item 2</li>
                        <li>Order item 3</li>
                        <li>Order item 4</li>
                    </ol>
                    <hr class="m-2"/>
                    <asp:Button runat="server" Text="Modify order" CssClass="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"/>
                </div>
            </aside>
            <section class="flex flex-col justify-center bg-lime-500 p-5 rounded w-full">
                mesas
            </section>
        </div>
    </section>
</asp:Content>
