using GameStore.Models;
using GameStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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
        protected IEnumerable<Game> GetGames() {
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
        }
    }
}