using GameStore.Models;
using GameStore.Models.Repository;
using GameStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace GameStore.Pages {
    public partial class Listing : System.Web.UI.Page {
        private readonly Repository _repository = new Repository();
        private readonly int _pageSize = 4;
        protected int CurrentPage {
            get {                
                int page = GetPageFromRequest();                
                return page > MaxPage ? MaxPage : page; 
            }
        }
        // свойство, возвращающее наибольший номер допустимой страницы
        protected int MaxPage {
            get {
                int prodCount = FilterGames().Count();
                return (int)Math.Ceiling((decimal)prodCount / _pageSize);
            }
        }
        private int GetPageFromRequest() {
            string reqValue = (string)RouteData.Values["page"] ??
                Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out int page) ? page : 1;
        }
        public IEnumerable<Game> GetGames() {
            return FilterGames()
                .OrderBy(g => g.GameId)
                .Skip((CurrentPage - 1) * _pageSize)
                .Take(_pageSize);
        }
        // вспомогательный метод для фильтрации игр по категориям
        private IEnumerable<Game> FilterGames() {
            IEnumerable<Game> games = _repository.Games;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            return currentCategory == null ? games :
                games.Where(p => p.Category == currentCategory);
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack) {
                int selectedGameId;
                if (int.TryParse(Request.Form["add"], out selectedGameId)) {
                    Game selectedGame = _repository.Games
                        .Where(g => g.GameId == selectedGameId).FirstOrDefault();

                    if (selectedGame != null) {
                        SessionHelper.GetCart(Session).AddItem(selectedGame, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL,
                            Request.RawUrl);

                        Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
    }
}