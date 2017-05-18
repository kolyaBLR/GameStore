using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository repositoryGame;
        private ICategoryRepository repositoryCagetory;

        public NavController(IGameRepository repositoryGame, ICategoryRepository repositoryCagetory)
        {
            this.repositoryGame = repositoryGame;
            this.repositoryCagetory = repositoryCagetory;
        }

        public PartialViewResult Menu(int categoryId = 0)
        {
            if (categoryId != 0)
            {
                ViewBag.SelectedCategory = repositoryCagetory.Category
                    .FirstOrDefault(c => c.CategoryId == categoryId)
                    .Name;
            }
            return PartialView(repositoryCagetory.Category);
        }
    }
}