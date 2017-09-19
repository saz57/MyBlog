using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;

using MyBlog.Models;
using MyBlog.ViewModels;
using MyBlog.DAL.Repository;
using MyBlog.DAL.RepositoriesManager;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                _signInManager = value;
            }
        }


        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (String.IsNullOrWhiteSpace(model.UserName) || String.IsNullOrWhiteSpace(model.Password)
                || RepositoryManager.UserManager.FindByName(model.UserName) != null || model.Password != model.ConfirmPassword)
            {
                return View();
            }

            ApplicationUser user = new ApplicationUser();
            user.UserName = model.UserName;
            user.IsBlocked = false;
            user.RegistrationDate = DateTime.Now;
            var result = RepositoryManager.UserManager.Create(user, model.Password);

            if(result.Succeeded)
            {
                RepositoryManager.UserManager.AddToRole(user.Id, "User");
                SignInManager.SignIn(user, false, false);
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
            if (!String.IsNullOrWhiteSpace(model.Login) && !String.IsNullOrWhiteSpace(model.Password))
            {

                ApplicationUser user = RepositoryManager.UserManager.FindByName(model.Login);

                if (user != null)
                {

                    if (user.IsBlocked)
                    {
                        return View("Blocked");
                    }

                    SignInStatus status = SignInManager.PasswordSignIn(model.Login, model.Password, false, false);

                    if (status == SignInStatus.Success)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }


        public ActionResult LogOut()
        {
            SignInManager.AuthenticationManager.SignOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}