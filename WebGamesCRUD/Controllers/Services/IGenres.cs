using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Models;

namespace WebGamesCRUD.Controllers.Services
{
    public interface IGenres
    {
        IEnumerable<Genre> AllGenres();
        void CreateGenre(Genre obj);
        void DeleteGenre(int genreId);
        void EditGenre(Genre obj);
        Genre FindById(int genreId);
    }
}
