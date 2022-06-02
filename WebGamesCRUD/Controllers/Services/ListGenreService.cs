using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Repository;
using System.Web;
using WebGamesCRUD.Models;
using Microsoft.EntityFrameworkCore;
using WebGamesCRUD.Models.View;

namespace WebGamesCRUD.Controllers.Services
{
    public class ListGenreService : IListGenres
    {
        private readonly GamesDbContext _db;
        public ListGenreService(GamesDbContext db)
        {
            _db = db;
        }
        public void CreateListGenre(GenreListGenre listgenre)
        {
            ListGenre lg = new ListGenre();
            foreach (var one in listgenre.GroupGenres)
            {
                if (one.Status == true)
                {
                    lg.IdListGenre = listgenre.IdGenreListGenre;
                    lg.IdGenre = one.IdGenre;
                    _db.ListGenres.Add(lg);
                    _db.SaveChanges();
                }
            }
        }
        public Tuple<List<ListGenre>, List<GameGenre>> AllListGenres()
        {
            var listGenres = _db.ListGenres;
            var query = from g in _db.Genres
                        join l in _db.ListGenres on g.IdGenre equals l.IdGenre
                        select new { l.IdListGenre, g.Name };
            var genres = query.ToList();

            List<GameGenre> gameGenres = new List<GameGenre>();
            foreach (var list in listGenres)
            {
                GameGenre view = new GameGenre();
                view.Id = list.IdListGenre;
                string temp = "";
                foreach (var genre in genres)
                {
                    if (list.IdListGenre == genre.IdListGenre)
                    {
                        temp += genre.Name + ",";
                    }
                }   
                view.Genres = temp.Trim(','); 
                if (!gameGenres.Exists(x => x.Id == view.Id))
                {
                    gameGenres.Add(view);
                }
            }
            var listgens = listGenres.ToList();
            listgens = listgens.GroupBy(x => x.IdListGenre).Select(z => z.First()).ToList();
            var tuple = new Tuple<List<ListGenre>, List<GameGenre>>(listgens, gameGenres);
            return tuple;
        }
        public void EditListGenre(GenreListGenre listgenre)
        {
            DeleteListGenre(listgenre.IdGenreListGenre); 
            ListGenre lg = new ListGenre();
            foreach (var one in listgenre.GroupGenres)
            {
                if (one.Status == true)
                {
                    lg.IdListGenre = listgenre.IdGenreListGenre;
                    lg.IdGenre = one.IdGenre;
                    _db.ListGenres.Add(lg);
                    _db.SaveChanges();
                }
            }
        }
        public void DeleteListGenre(int listGenreId)
        {
            _db.Database.ExecuteSqlRaw("Delete ListGenre where ID_ListGenre = {0}", listGenreId);
        }
        public GenreListGenre SendViewListGenreCreate()
        {
            var index = _db.ListGenres.Select(x => x.IdListGenre).Max();
            var listGenres = _db.Genres;
            GenreListGenre glg = new GenreListGenre();
            glg.IdGenreListGenre = ++index;
            List<ToListGenre> gigaToList = new List<ToListGenre>();
            foreach (var gen in listGenres)
            {
                ToListGenre to = new ToListGenre();
                to.IdGenre = gen.IdGenre;
                to.Name = gen.Name;
                to.Status = false;
                gigaToList.Add(to);
            }
            glg.GroupGenres = gigaToList;
            return glg;
        }
        public GenreListGenre SendViewListGenreEdit(int listGenreId)
        {
            var list = _db.ListGenres;
            ListGenre oneobj = list.Where(x => x.IdListGenre == listGenreId).First();
            GenreListGenre glg = new GenreListGenre();
            glg.IdGenreListGenre = oneobj.IdListGenre;
            var listGenres = _db.Genres.ToList();
            var query = (from lg in _db.ListGenres
                         join g in _db.Genres on lg.IdGenre equals g.IdGenre
                         where lg.IdListGenre == listGenreId
                         select new { g.IdGenre, g.Name }).ToList();
            List<ToListGenre> gigaToList = new List<ToListGenre>();
            foreach (var gen in listGenres)
            {
                if (query.Select(x => x).Any(z => z.Name == gen.Name))
                {
                    ToListGenre to = new ToListGenre();
                    to.IdGenre = gen.IdGenre;
                    to.Name = gen.Name;
                    to.Status = true;
                    gigaToList.Add(to);
                }
                else
                {
                    ToListGenre to = new ToListGenre();
                    to.IdGenre = gen.IdGenre;
                    to.Name = gen.Name;
                    to.Status = false;
                    gigaToList.Add(to);
                }
            }
            glg.GroupGenres = gigaToList;
            return glg;
        }
        public ListGenre FindById(int listGenreId)
        {
            var list = _db.ListGenres;
            ListGenre obj = list.Where(x => x.IdListGenre == listGenreId).First();
            return obj;
        }
    }
}
