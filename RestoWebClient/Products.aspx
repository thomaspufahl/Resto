<%@ Page Title="" Language="C#" MasterPageFile="~/Resto.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="RestoWebClient.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-blue-950 p-4 text-white">
        <div class="flex justify-between items-center">
            <h2 class="text-2xl font-bold">Lista de Productos</h2>
            <div class="flex space-x-4">
                <asp:Button ID="AddProduct" OnClick="AddProduct_Click" CssClass="bg-purple-400 text-white px-4 py-2 rounded" runat="server" Text="Agregar Producto" />
                <asp:Button ID="UpdateProduct" CssClass="bg-purple-400 text-white px-4 py-2 rounded" runat="server" Text="Modificar Producto" />
                <asp:Button ID="DeleteProduct" CssClass="bg-red-500 text-white px-4 py-2 rounded" runat="server" Text="Eliminar Producto" />
            </div>
        </div>
    </div>

    <div class="bg-stone-200 p-4 shadow-md rounded-md overflow-x-auto">
        <table class="w-full">
            <thead>
                <tr>
                    <th class="text-left text-dark">Nombre</th>
                    <th class="text-left text-dark">Categoría</th>
                    <th class="text-left text-dark">Descripción</th>
                    <th class="text-left text-dark">Stock</th>
                    <th class="text-left text-dark">Stock Mínimo</th>
                    <th class="text-left text-dark">Precio</th>
                    <th class="text-left text-dark"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="ProductList">
                    <ItemTemplate>
                        <tr>
                            <td class="text-sm font-semibold leading-6 text-gray-900"><%# Eval("ProductName") %></td>
                            <td class="mt-1 truncate text-xs leading-5 text-gray-500"><%# RestoWebClient.ProductCategoryConverter.Convert((int)Eval("ProductCategoryId")) %></td>
                            <td class="mt-1 truncate text-xs leading-5 text-gray-500"><%# Eval("ProductDescription") %></td>
                            <td class="mt-1 truncate text-xs leading-5 text-gray-500"><%# Eval("Stock") %></td>
                            <td class="mt-1 truncate text-xs leading-5 text-gray-500"><%# Eval("MinStockLevel") %></td>
                            <td class="mt-1 truncate text-xs leading-5 text-gray-500"><%# RestoWebClient.UnitPriceConverter.Convert((decimal)Eval("UnitPrice")) %></td>
                            <td class="mt-1 truncate text-xs leading-5 text-gray-500">
                                <asp:Button ID="UpdateProduct" OnClick="UpdateProduct_Click" CommandName="UpdateProductArg" CommandArgument=<%#Eval("ProductId")%> CssClass="bg-purple-400 text-white px-4 py-2 rounded" runat="server" Text="Modificar" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
