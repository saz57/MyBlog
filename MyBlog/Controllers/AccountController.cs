using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBlog.Models;
using MyBlog.ViewModels;
using MyBlog.Repository;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private static UsersRepository _userRepository;

        static AccountController()
        {
            _userRepository = new UsersRepository();
        }



        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationViewModel model)
        {

            if (_userRepository.GetByLogin(model.Login) != null)
            {
                return View();
            }

            if (_userRepository.GetByNickName(model.NickName) != null)
            {
                return View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                return View();
            }

            ApplicationUser user = new ApplicationUser();
            user.Login = model.Login;
            user.Password = model.Password;
            user.NickName = model.NickName;
            _userRepository.Put(user);

            if (_userRepository.GetByLogin(user.Login) != null)
            {
                FormsAuthentication.SetAuthCookie(model.NickName, true);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            ApplicationUser user = _userRepository.GetByLogin(model.Login);

            if (user != null && user.Password == model.Password)
            {
                FormsAuthentication.SetAuthCookie(user.NickName, true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}