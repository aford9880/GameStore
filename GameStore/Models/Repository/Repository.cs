using System.Collections.Generic;

namespace GameStore.Models.Repository {
    public class Repository {
        private readonly EFDbContext context = new EFDbContext();
        public IEnumerable<Game> Games {
            get { return context.Games; }
        }
    }
}