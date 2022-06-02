using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebGamesCRUD.Models;
using WebGamesCRUD.Repository;

namespace WebGamesCRUD.Controllers.Services
{
    public class GenreService : IGenres
    {
        private readonly GamesDbContext _db;
        public GenreService(GamesDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Genre> AllGenres()
        {
            var genres = _db.Genres;
            return genres;
        }
        public void CreateGenre(Genre obj)
        {
            _db.Genres.Add(obj);
            _db.SaveChanges();
        }
        public void DeleteGenre(int genreId)
        {
            var listGenre = _db.ListGenres;
            if (listGenre.Any(x => x.IdGenre == genreId) == false)
            {
                var obj = _db.Genres.Find(genreId);
                _db.Genres.Remove(obj);
                _db.SaveChanges();
            }
        }
        public void EditGenre(Genre obj)
        {
            _db.Genres.Update(obj);
            _db.SaveChanges();
        }
        public Genre FindById(int genreId)
        {
            var obj = _db.Genres.Find(genreId);
            return obj;
        }
    }
}
