using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Models;
using WebGamesCRUD.Models.View;

namespace WebGamesCRUD.Controllers.Services
{
    public interface IGames
    {
        Tuple<List<Game>, List<GameGenre>> AllGames();
        Game_ListGenre_Genre SendViewGame(int gameId);
        void CreateGame(Game_ListGenre_Genre obj);
        void DeleteGame(int gameId); 
        void EditGame(Game_ListGenre_Genre obj);
        Game FindById(int gameId);
    }
}
