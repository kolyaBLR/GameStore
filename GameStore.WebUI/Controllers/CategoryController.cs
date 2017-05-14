using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View(repository.Category);
        }

        public ActionResult Edit(int categoryId)
        {
            Category category = repository.Category.FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(category);
                TempData["message"] = string.Format("Изменения в в категории \"{0}\" были сохранены", category.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Создание категории:";
            return View("Edit", new Category());
        }

        public ActionResult Delete(int categoryId)
        {
            Category deleted = repository.DeleteCategory(categoryId);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Категория \"{0}\" была удалена", deleted.Name);
            }
            return RedirectToAction("Index");
        }
    }
}