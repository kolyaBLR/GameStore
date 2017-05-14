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
        IGameRepository repository;
        private int pageSize = 10;

        public AdminController(IGameRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel()
            {
                Games = repository.Games
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Games.Count()
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
            Game game = repository.Games.FirstOrDefault(g => g.GameId == gameId);

            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }
                repository.SaveGame(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(game);
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
            Game deletedGame = repository.DeleteGame(gameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена", deletedGame.Name);
            }
            return RedirectToAction("Index");
        }
    }
}