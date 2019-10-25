<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="GameStore.Pages.Listing" 
     MasterPageFile="~/Pages/Store.Master" %>
<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <%
            foreach (GameStore.Models.Game game in GetGames())
            {
                Response.Write(String.Format(@"
                    <div class='item'>
                        <h3>{0}</h3>
                        {1}
                        <h4>{2:c}</h4>
                    </div>", 
                    game.Name, game.Description, game.Price));
            }
        %>
    </div>    
    <div class="pager">
        <%
            for (int i = 1; i <= MaxPage; i++)
            {
                string path = RouteTable.Routes.GetVirtualPath(null, null,
                    new RouteValueDictionary() { { "page", i } }).VirtualPath;
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