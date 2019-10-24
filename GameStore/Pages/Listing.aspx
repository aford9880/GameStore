<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="GameStore.Pages.Listing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gamestore</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
    </form>
    <div>
        <%
            for (int i = 1; i <= MaxPage; i++)
            {
                Response.Write(
                    String.Format("<a href='/Pages/Listing.aspx?page={0}' {1}>{2}</a>{3}",
                        /*0*/ i, // текущий номер страницы в цикле
                        /*1*/ i == CurrentPage ? "class='selected'" : "", // если текущая страница, то выделяем ее
                        /*2*/ i, // текст ссылки (в нашем случае номер страницы)
                        /*3*/ i < MaxPage ? " | " : "" // финтифлюшки для удобства
                        ));                
            }
        %>
    </div>
</body>
</html>
