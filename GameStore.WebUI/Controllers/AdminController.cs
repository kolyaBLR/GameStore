using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGameRepository gameRepository;
        ICategoryRepository categoryRepository;
        private int pageSize = 10;

        public AdminController(IGameRepository gameRepository, ICategoryRepository categoryRepository)
        {
            this.gameRepository = gameRepository;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index(int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel()
            {
                Games = gameRepository.Games
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = gameRepository.Games.Count()
                }
            };
            if (page != 1 && (page <= 0 || page > model.PagingInfo.TotalPages))
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Edit(int gameId = 0)
        {
            ViewBag.Title = "Редактирование игры";
            CreateGameViewModel model = new CreateGameViewModel(categoryRepository.Category, gameId)
            {
                Game = gameRepository.Games.FirstOrDefault(g => g.GameId == gameId)
            };
            if (gameId == 0 || model.Game == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(string category, Game game = null)
        {
            if (game == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                game.CategoryId = categoryRepository.Category.FirstOrDefault(c => c.Name == category).CategoryId;
                gameRepository.SaveGame(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(new CreateGameViewModel(categoryRepository.Category)
                {
                    Game = game
                });
            }
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Создание игры";
            var model = new CreateGameViewModel(categoryRepository.Category)
            {
                Game = new Game(),
            };
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Game deletedGame = gameRepository.DeleteGame(gameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена", deletedGame.Name);
            }
            return RedirectToAction("Index");
        }
    }
}