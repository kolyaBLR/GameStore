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
            GamesListViewModel model = new GamesListViewModel()
            {
                Games = repository.Games
                .Where(p => category == null || p.Category == category)
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Games.Count() :
                    repository.Games.Where(game => game.Category == category).Count()
                }
            };

            if (page != 1 && (page <= 0 || page > model.PagingInfo.TotalPages))
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}