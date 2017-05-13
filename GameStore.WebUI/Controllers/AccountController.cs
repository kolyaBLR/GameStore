using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Authentication;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository repository;

        public AccountController(IUserRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User DbUser = repository.User.FirstOrDefault(u => u.Email + u.Passwod == model.Email + model.Password);
                if (DbUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("List", "Game");
                }
                else
                {
                    ModelState.AddModelError("errorPassword", "Неправильный логин или пароль.");
                    return View(model);
                }
            }
            return View(model);
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User DbUser = repository.User.FirstOrDefault(u => u.Email == user.Email);
                    if (DbUser.Email == user.Email)
                    {
                        ModelState.AddModelError("errorEmail", "Данный email уже зарегестрирован. Попробуйте восстановить пароль.");
                    }
                }
                catch
                {
                    repository.SaveUser(user);
                    TempData["message"] = string.Format("Пользователь {0} {1} зарегестрирован.", user.FirstName, user.LastName);
                    return RedirectToAction("Login");
                }
            }
            return View(user);
        }
    }
}