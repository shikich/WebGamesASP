using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Controllers.Services;
using WebGamesCRUD.Models;
using WebGamesCRUD.Models.View;

namespace WebGamesCRUD.Controllers
{
    public class ListGenreController : Controller
    {
        private readonly IListGenres _listGenres;
        public ListGenreController(IListGenres games)
        {
            _listGenres = games;
        }        
        //GET
        public IActionResult List()
        {
            var listGenres = _listGenres.AllListGenres();
            return View(listGenres);
        }
        //GET
        public IActionResult Create()
        {
            var obj = _listGenres.SendViewListGenreCreate();
            return View(obj);
        }      
        [HttpPost]
        public IActionResult Create(GenreListGenre obj)
        {
            _listGenres.CreateListGenre(obj);
            return RedirectToAction("List");
        }
        //GET
        public IActionResult Edit(int listGenreId)
        {
            var obj = _listGenres.SendViewListGenreEdit(listGenreId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(GenreListGenre listgenre)
        {
            _listGenres.EditListGenre(listgenre);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int listGenreId)
        {
            var obj = _listGenres.FindById(listGenreId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeletePost(int listGenreId)
        {
            _listGenres.DeleteListGenre(listGenreId);
            return RedirectToAction("List");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
