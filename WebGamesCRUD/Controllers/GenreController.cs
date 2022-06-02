using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebGamesCRUD.Controllers.Services;
using WebGamesCRUD.Models;

namespace WebGamesCRUD.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenres _genres;
        public GenreController(IGenres games)
        {
            _genres = games;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre obj)
        {
            _genres.CreateGenre(obj);
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            var genres = _genres.AllGenres();
            return View(genres);
        }
        public IActionResult Edit(int genreId)
        {
            var obj = _genres.FindById(genreId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Genre obj)
        {
            _genres.EditGenre(obj);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int genreId)
        {
            var obj = _genres.FindById(genreId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeletePost(int genreId)
        {
            _genres.DeleteGenre(genreId);
            return RedirectToAction("List");     
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
