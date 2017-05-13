using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private IGameRepository repository;
        private int pageSize = 5;

        public GameController(IGameRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult List(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel();
            model.Games = repository.Games
                .Where(p => category == null || p.Category == category)
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            PagingInfo pagination = new PagingInfo();
            pagination.CurrentPage = page;
            pagination.ItemsPerPage = pageSize;
            pagination.TotalItems = category == null ?
                repository.Games.Count() :
                repository.Games.Where(game => game.Category == category).Count();

            model.PagingInfo = pagination;
            model.CurrentCategory = category;

            if (page <= 0 || page > pagination.TotalPages)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public FileContentResult GetImage(int gameId)
        {
            Game game = repository.Games.FirstOrDefault(g => g.GameId == gameId);
            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}