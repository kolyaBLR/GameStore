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

        public ActionResult Edit(int gameId)
        {
            ViewBag.Title = "Редактирование игры";
            CreateGameViewModel model = new CreateGameViewModel()
            {
                Game = gameRepository.Games.FirstOrDefault(g => g.GameId == gameId),
                Category = categoryRepository.Category                
            };

            if (model.Game == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(CreateGameViewModel model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.Game.ImageMimeType = image.ContentType;
                    model.Game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.Game.ImageData, 0, image.ContentLength);
                }
                gameRepository.SaveGame(model.Game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", model.Game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(model);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Создание игры";
            return View("Edit", new Game());
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