using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Models;
using WebGamesCRUD.Models.View;

namespace WebGamesCRUD.Controllers.Services
{
    public interface IListGenres
    {
        Tuple<List<ListGenre>, List<GameGenre>> AllListGenres();
        void CreateListGenre(GenreListGenre obj);
        void DeleteListGenre(int listGenreId);
        void EditListGenre(GenreListGenre listgenre);
        GenreListGenre SendViewListGenreCreate();
        GenreListGenre SendViewListGenreEdit(int listGenreId);
        ListGenre FindById(int listGenreId);
    }
}
