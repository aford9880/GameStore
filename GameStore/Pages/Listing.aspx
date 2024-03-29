﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="GameStore.Pages.Listing" 
     MasterPageFile="~/Pages/Store.Master" %>
<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
         <asp:Repeater ItemType="GameStore.Models.Game"
            SelectMethod="GetGames" runat="server">
            <ItemTemplate>
                <div class="item">
                    <h3><%# Item.Name %></h3>
                    <%# Item.Description %>
                    <h4><%# Item.Price.ToString("c") %></h4>
                    <button name="add" type="submit" value="<%# Item.GameId %>">
                        Добавить в корзину
                    </button>
                </div>
            </ItemTemplate>
        </asp:Repeater>                
    </div>    
    <div class="pager">
                <%
            for (int i = 1; i <= MaxPage; i++)
            {
                string category = (string)Page.RouteData.Values["category"]
                    ?? Request.QueryString["category"];                
                string path = RouteTable.Routes.GetVirtualPath(null, null,
                    new RouteValueDictionary() { {"category", category}, { "page", i } }).VirtualPath;
                Response.Write(
                    String.Format("<a href='{0}' {1}>{2}</a>{3}",
                        /*0*/ path, // текущий номер страницы в цикле
                        /*1*/ i == CurrentPage ? "class='selected'" : "", // если текущая страница, то выделяем ее
                        /*2*/ i, // текст ссылки (в нашем случае номер страницы)
                        /*3*/ i < MaxPage ? " | " : "" // финтифлюшки для удобства
                        ));  
            }
        %>
    </div>
</asp:Content>