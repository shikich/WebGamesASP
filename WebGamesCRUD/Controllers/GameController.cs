using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Controllers.Services;
using WebGamesCRUD.Models;
using WebGamesCRUD.Models.View;
using WebGamesCRUD.Repository;

namespace WebGamesCRUD.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGames _games;
        public GameController(IGames games, ILogger<GameController> logger)
        {
            _games = games;  
            _logger = logger;
        }      
        //GET
        public IActionResult List()
        {
            var games = _games.AllGames();
            return View(games);
        }
        //GET
        public IActionResult Create()
        {
            var obj = _games.SendViewGame(0);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Create(Game_ListGenre_Genre obj)
        {
            _games.CreateGame(obj);
            return RedirectToAction("List");
        }
        //GET
        public IActionResult Edit(int gameId)
        {
            var obj = _games.SendViewGame(gameId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Game_ListGenre_Genre obj)
        {
            _games.EditGame(obj);
            return RedirectToAction("List");
        }
        //GET
        public IActionResult Delete(int gameId)
        {
            var obj = _games.FindById(gameId);
            return View(obj);
        }
        [HttpPost]
        public IActionResult DeletePost(int gameId)
        {
            _games.DeleteGame(gameId);
            return RedirectToAction("List");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
