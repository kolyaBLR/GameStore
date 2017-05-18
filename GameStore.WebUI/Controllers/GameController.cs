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
        private IGameRepository repositoryGame;
        private ICategoryRepository repositoryCategory;
        private int pageSize = 5;

        public GameController(IGameRepository repositoryGame, ICategoryRepository repositoryCategory)
        {
            this.repositoryGame = repositoryGame;
            this.repositoryCategory = repositoryCategory;
        }

        public ActionResult List(int categoryId = 0, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel()
            {
                Games = repositoryGame.Games
                .Where(p => categoryId == 0 || p.CategoryId == categoryId)
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = categoryId == 0 ?
                    repositoryGame.Games.Count() :
                    repositoryGame.Games.Where(game => game.CategoryId == categoryId).Count()
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