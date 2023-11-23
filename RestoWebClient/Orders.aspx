<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RestoWebClient.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="flex mx-auto mt-2 gap-4 flex-wrap">
        <header class="flex flex-row bg-white p-5 rounded w-full">
            header
        </header>
        <div class="flex justify-center bg-white p-5 rounded w-full gap-2">
            <aside class="flex flex-col justify-center bg-lime-200 p-5 rounded w-full max-w-md">
                <div>
                    <% if (SelectedOrder == null)
                        { %>
                    <h2>No order selected</h2>
                    <% }
                        else
                        { %>
                    <h2>Table: <%=SelectedOrder.TableNumber%></h2>
                    <h2>Order Number: <%=SelectedOrder.OrderNumber%></h2>
                    <hr class="m-2" />

                    <ol>
                        <% foreach (var item in SelectedOrderItemList)
                            {%>
                        <li><%= RestoWebClient.ProductIdConverter.Convert(item.ProductId)%></li>
                        <% } %>
                    </ol>
                    <hr class="m-2" />
                    <asp:Button runat="server" ID="BtnModifyOrder" OnClick="BtnModifyOrder_Click" CommandName="BtnModifyOrderArg"
                        Text="Modify order"
                        CssClass="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600" />
                    <% } %>
                </div>
            </aside>
            <section class="flex flex-col justify-center bg-lime-500 p-5 rounded w-full">
                <ol role="list" class="divide-y divide-gray-100">
                    <asp:Repeater runat="server" ID="rptRestoTables">
                        <ItemTemplate>
                            <li class="flex justify-between gap-x-6 py-5">
                                <div class="flex min-w-0 gap-x-4">
                                    <div class="min-w-0 flex-auto">
                                        <p class="text-sm font-semibold leading-6 text-gray-900">Tabler number: <%#Eval("TableNumber")%></p>
                                        <asp:Label runat="server" ID="LblOrderNumber" Visible='<%#Eval("OrderNumber").ToString() != "-1" %>' Text='<%#Eval("OrderNumber") %>' CssClass="mt-1 truncate text-xs leading-5 text-gray-500"></asp:Label>
                                    </div>
                                </div>
                                <div class="hidden shrink-0 sm:flex sm:flex-col sm:items-end">
                                    <asp:Button runat="server" ID="BtnAddOrder" Visible='<%#Eval("OrderNumber").ToString() == "-1" %>'
                                        Text="Nuevo pedido" OnClick="BtnAddOrder_Click" CssClass="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600" />

                                    <asp:Button runat="server" ID="BtnSeeOrder" Visible='<%#Eval("OrderNumber").ToString() != "-1" %>'
                                        Text="Ver pedido" OnClick="BtnSeeOrder_Click" CommandName="BtnSeeOrderArg" CommandArgument='<%#Eval("OrderNumber") %>' CssClass="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600" />
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ol>
            </section>
        </div>
    </section>
</asp:Content>
