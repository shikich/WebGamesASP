using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Models;
using WebGamesCRUD.Models.View;
using WebGamesCRUD.Repository;

namespace WebGamesCRUD.Controllers.Services
{
    public class GameService : IGames
    {
        private readonly GamesDbContext _db;
        public GameService(GamesDbContext db)
        {
            _db = db;
        }
        public Tuple<List<Game>, List<GameGenre>> AllGames()
        {
            var games = _db.Games;
            var query = from g in _db.Genres
                         join l in _db.ListGenres on g.IdGenre equals l.IdGenre
                         select new { l.IdListGenre, g.Name };
            var genres = query.ToList();

            List<GameGenre> gameGenres = new List<GameGenre>();
            foreach (var game in games)
            {
                GameGenre view = new GameGenre();
                view.Id = game.IdGame;
                string temp = "";
                foreach (var genre in genres)
                {
                    if (game.IdListGenre == genre.IdListGenre)
                    {
                        temp += genre.Name + ",";
                    }
                }
                view.Genres = temp.Trim(',');
                gameGenres.Add(view);
            }
            var tuple = new Tuple<List<Game>, List<GameGenre>>(games.ToList(), gameGenres);
            return tuple;
        }
        public void CreateGame(Game_ListGenre_Genre obj)
        {
            Game g = new Game();
            g.Name = obj.Name;
            g.Developer = obj.Developer;
            g.IdListGenre = obj.IdListGenre;
            _db.Games.Add(g);
            _db.SaveChanges();
        }
        public void DeleteGame(int gameId)
        {
            var obj = _db.Games.Find(gameId);
            _db.Games.Remove(obj);
            _db.SaveChanges();
        }
        public void EditGame(Game_ListGenre_Genre obj)
        {
            if(obj.IdGame != 0)
            {
                Game g = new Game();
                g.IdGame = obj.IdGame;
                g.Name = obj.Name;
                g.Developer = obj.Developer;
                g.IdListGenre = obj.IdListGenre;
                _db.Games.Update(g);
                _db.SaveChanges();
            }
        }
        public Game_ListGenre_Genre SendViewGame(int gameId)
        {
            Game obj = new Game { IdGame = 0 };
            if (gameId != 0)
            {
                obj = _db.Games.Find(gameId);
            }
            List<ListGenre> listListGenre = new List<ListGenre>();
            List<GameGenre> gameGenres = new List<GameGenre>();

            var listGenres = _db.ListGenres;
            var query = from g in _db.Genres
                        join l in _db.ListGenres on g.IdGenre equals l.IdGenre
                        select new { l.IdListGenre, g.IdGenre, g.Name };
            var genres = query.ToList(); 

            Game_ListGenre_Genre all = new Game_ListGenre_Genre();
            all.IdGame = obj.IdGame;
            all.Name = obj.Name;
            all.Developer = obj.Developer;
            all.IdListGenre = obj.IdListGenre;
            foreach (var list in listGenres)
            {
                ListGenre l = new ListGenre();
                l.IdListGenre = list.IdListGenre;
                l.IdGenre = list.IdGenre;
                GameGenre gg = new GameGenre();
                gg.Id = list.IdListGenre;
                string temp = "";
                foreach (var genre in genres)
                {
                    if (list.IdListGenre == genre.IdListGenre)
                    {
                        temp += genre.Name + ",";
                    }
                    gg.Genres = temp.Trim(',');
                    if (!gameGenres.Exists(x => x.Id == gg.Id))
                    {
                        gameGenres.Add(gg);
                    }
                }
                if (!listListGenre.Exists(x => x.IdListGenre == list.IdListGenre))
                {
                    listListGenre.Add(l);
                }
            } // manage listGenres for read
            all.GroupGenres = gameGenres;
            all.GroupListGenre = listListGenre;
            var listgens = listGenres.ToList();
            return all;
        }
        public Game FindById(int gameId)
        {
            var obj = _db.Games.Find(gameId);
            return obj;
        }
    }
}
