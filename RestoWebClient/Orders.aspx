<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RestoWebClient.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="flex mx-auto gap-4 h-[calc(100vh-124px)]">
                <div class="flex justify-center w-full gap-x-6">
                    <!-- ORDER VIEW -->
                    <aside class="flex flex-col bg-white justify-start p-5 rounded w-full max-w-[30rem] shadow">
                        <% if (SelectedOrder == null)
                            { %>
                        <h2 class="text-3xl font-bold text-gray-900 mb-4 text-center">No order selected</h2>
                        <% }
                            else
                            { %>
                        <h2 class="text-3xl font-bold text-gray-900 mb-4">Table: <%=SelectedOrder.TableNumber%></h2>
                        <h2 class="text-3xl font-bold text-gray-900 mb-4">Order Number: <%=SelectedOrder.OrderNumber%></h2>
                        <% if (RestoWebClient.SessionManager.IsLoggedAsManager)
                            { %>
                        <h2 class="text-3xl font-bold text-gray-900">Waiter: <%=RestoWebClient.EmployeeConverter.Convert(SelectedOrder.EmployeeId)%></h2>
                        <span class="text-lg font-medium text-gray-700 mb-4">Number: <%=RestoWebClient.EmployeeConverter.Number(SelectedOrder.EmployeeId)%></span>
                        <% } %>
                        <asp:Button runat="server" ID="BtnModifyOrder" OnClick="BtnModifyOrder_Click" CommandName="BtnModifyOrderArg"
                            Text="Modify order"
                            CssClass="flex w-full justify-center hover:cursor-pointer rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-slate-100 shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600" />
                        <hr class="m-4" />
                        <ul class="flex flex-col pl-6 list-disc overflow-y-scroll max-h-fit">
                            <% foreach (var item in SelectedOrderItemList)
                                {%>
                            <li class="text-lg text-gray-700 mb-4"><%= RestoWebClient.ProductIdConverter.Convert(item.ProductId)%></li>
                            <% } %>
                        </ul>
                        <% } %>
                    </aside>

                    <!-- TABLES -->
                    <section class="flex flex-col gap-6 rounded w-full">
                        <header class="flex bg-white shadow rounded-lg w-full">
                            <div role="search" class="w-full">
                                <label for="default-search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
                                <div class="relative">
                                    <div class="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
                                        <svg class="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20">
                                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z" />
                                        </svg>
                                    </div>
                                    <asp:TextBox runat="server" ID="Search" OnTextChanged="Search_TextChanged" AutoPostBack="true" CssClass="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search Order, Table..." />
                                    <button runat="server" id="BtnSearch"  class="text-white absolute end-2.5 bottom-2.5 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Search</button>
                                </div>
                            </div>
                        </header>
                        <ol role="list" class="flex flex-wrap gap-6 overflow-y-scroll max-h-fit py-4">
                            <asp:Repeater runat="server" ID="rptRestoTables">
                                <ItemTemplate>
                                    <li class="bg-white rounded-lg overflow-hidden shadow w-80 h-fit">
                                        <asp:Panel runat="server" ID="StatusBar" CssClass='<%# UpdateTableStatusBar((byte)Eval("TableNumber"))%>'>
                                        </asp:Panel>
                                        <section class="p-4">
                                            <h2 class="text-3xl font-bold text-gray-900 mb-4">Table <%#Eval("TableNumber")%></h2>
                                            <asp:Label runat="server" ID="LblOrderNumber" Text='<%#Eval("OrderNumber").ToString() != "-1" ? "Order: " + Eval("OrderNumber") : "No order"%>'
                                                Visible='<%#Eval("IsActive")%>'
                                                CssClass="mt-1 block truncate text-lg text-gray-700" />
                                            <asp:Label runat="server" ID="LblOrderStatus" Text='<%# "Status: " + UpdateOrderStatus((long)Eval("OrderNumber"))%>'
                                                Visible='<%#Eval("IsActive")%>'
                                                CssClass="truncate block text-lg text-gray-700" />
                                            <asp:Label runat="server" ID="LblOrderDate" Text='<%# "Order date: " + GetOrderDate((long)Eval("OrderNumber"))%>'
                                                Visible='<%#Eval("IsActive")%>'
                                                CssClass="truncate block text-lg text-gray-700" />
                                            <asp:Label runat="server" ID="LblOrderUpdatedAt" Text='<%# "Last update: " + GetOrderDate((long)Eval("OrderNumber"))%>'
                                                Visible='<%#Eval("IsActive")%>'
                                                CssClass="truncate block text-lg text-gray-700" />
                                        </section>
                                        <footer class="p-4">
                                            <div class="hidden shrink-0 flex flex-col gap-2 sm:flex sm:flex-col sm:items-end">
                                                <asp:Button runat="server" ID="BtnAddOrder" Visible='<%#Eval("OrderNumber").ToString() == "-1" && (bool)Eval("IsActive")%>'
                                                    Text="Nuevo pedido" OnClick="BtnAddOrder_Click"
                                                    CssClass="flex w-full hover:cursor-pointer justify-center rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600" />

                                                <asp:Button runat="server" ID="BtnSeeOrder" Visible='<%#Eval("OrderNumber").ToString() != "-1" && (bool)Eval("IsActive")%>'
                                                    Text="Ver pedido" OnClick="BtnSeeOrder_Click" CommandName="BtnSeeOrderArg" CommandArgument='<%#Eval("OrderNumber") %>'
                                                    CssClass="flex w-full hover:cursor-pointer justify-center rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600" />

                                                <asp:Button runat="server" ID="BtnActivateTable" Visible='<%#!(bool)Eval("IsActive") && RestoWebClient.SessionManager.IsLoggedAsManager%>'
                                                    Text="Habilitar mesa" OnClick="BtnActivateTable_Click" CommandName="BtnActivateTableArg" CommandArgument='<%#Eval("TableNumber") %>'
                                                    CssClass="flex w-full hover:cursor-pointer justify-center rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600" />

                                                <asp:Button runat="server" ID="BtnDeactivateTable" Visible='<%#Eval("OrderNumber").ToString() == "-1" && (bool)Eval("IsActive") && RestoWebClient.SessionManager.IsLoggedAsManager%>'
                                                    Text="Deshabilitar mesa" OnClick="BtnDeactivateTable_Click" CommandName="BtnDeactivateTableArg" CommandArgument='<%#Eval("TableNumber") %>'
                                                    CssClass="flex w-full hover:cursor-pointer justify-center rounded-md bg-blue-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-blue-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600" />

                                                <asp:Button runat="server" ID="BtnDeleteTable" Visible='<%#!(bool)Eval("IsActive") && RestoWebClient.SessionManager.IsLoggedAsManager%>'
                                                    Text="Eliminar mesa" OnClick="BtnDeleteTable_Click" CommandName="BtnDeleteTableArg" CommandArgument='<%#Eval("TableNumber") %>'
                                                    CssClass="flex w-full hover:cursor-pointer justify-center rounded-md bg-red-700 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-red-800 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-red-600" />
                                            </div>
                                        </footer>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (RestoWebClient.SessionManager.IsLoggedAsManager)
                                { %>
                            <li class="overflow-hidden w-80 h-fit">
                                <button runat="server" id="BtnAddRestoTable" title="Add table" onserverclick="BtnAddRestoTable_ServerClick" class="hover:cursor-pointer rounded hover:ring-2 hover:ring-blue-800 hover:ring-inset">
                                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-new-section text-gray-700 hover:text-gray-800 p-10" width="320" height="316" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M9 12l6 0" />
                                        <path d="M12 9l0 6" />
                                        <path d="M4 6v-1a1 1 0 0 1 1 -1h1m5 0h2m5 0h1a1 1 0 0 1 1 1v1m0 5v2m0 5v1a1 1 0 0 1 -1 1h-1m-5 0h-2m-5 0h-1a1 1 0 0 1 -1 -1v-1m0 -5v-2m0 -5" />
                                    </svg>
                                </button>
                            </li>
                            <% } %>
                        </ol>
                    </section>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
