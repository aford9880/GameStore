using GameStore.Models;
using GameStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Pages {
    public partial class Listing : System.Web.UI.Page {
        private readonly Repository _repository = new Repository();
        private int _pageSize = 4;
        protected int CurrentPage {
            get {
                int page;
                // если не удалось преобразовать page в int, то выводим 1 страницу
                page = int.TryParse(Request.QueryString["page"], out page /*выведет 2, если в конце url будет "?page=2"*/) ? page : 1;
                return page > MaxPage ? MaxPage : page; 
            }
        }
        // свойство, возвращающее наибольший номер допустимой страницы
        protected int MaxPage {
            get {
                return (int)Math.Ceiling((decimal)_repository.Games.Count() / _pageSize);
            }
        }
        protected IEnumerable<Game> GetGames() {
            return _repository.Games
                .OrderBy(g => g.GameId)
                .Skip((CurrentPage - 1) * _pageSize)
                .Take(_pageSize);
        }
        protected void Page_Load(object sender, EventArgs e) {
        }
    }
}