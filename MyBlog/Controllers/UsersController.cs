using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

using MyBlog.DAL.RepositoriesManager;
using MyBlog.Models;
using MyBlog.ViewModels;

namespace MyBlog.Controllers
{
    
    public class UsersController : Controller
    {
       
        private int pageSize = 20;

        [Authorize(Roles = "Admin")]
        public ActionResult Index(int currentPage = 1)
        {

            UsersListViewModel viewModel = new UsersListViewModel();
            List<UserViewModel> userViews = new List<UserViewModel>();
            List<ApplicationUser> users = RepositoryManager.UserManager.Users.ToList();
            List<SelectListItem> roleViews = new List<SelectListItem>();

            foreach (IdentityRole role in RepositoryManager.RoleManager.Roles.ToList())
            {
                roleViews.Add(new SelectListItem() { Text = role.Name, Value = role.Name });
            }


            foreach (ApplicationUser user in users)
            {
                userViews.Add(new UserViewModel(user, RepositoryManager.UserManager.GetRoles(user.Id)));
            }
            viewModel.Users = new PagedList<UserViewModel>(userViews, currentPage, pageSize);
            viewModel.Roles = roleViews;

            return View("Index", viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShowUserProfile(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {

                ApplicationUser user = RepositoryManager.UserManager.Users.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    UserViewModel viewModel = new UserViewModel(user, RepositoryManager.UserManager.GetRoles(user.Id));
                    return View("UserView", viewModel);
                }

            }
            return Index();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveFromRole(string userId, string role)
        {
            if (!String.IsNullOrWhiteSpace(userId) && !String.IsNullOrWhiteSpace(role) && RepositoryManager.UserManager.FindById(userId) != null)
            {
                RepositoryManager.UserManager.RemoveFromRole(userId, role);
            }
            return Index();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddToRole(string userId, string role)
        {
            if (!String.IsNullOrWhiteSpace(userId) && !String.IsNullOrWhiteSpace(role) && RepositoryManager.UserManager.FindById(userId) != null) // maybe useless
            {
                RepositoryManager.UserManager.AddToRole(userId, role);
            }
            return Index();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChangeIsBlocked(string userId, bool isBlocked = false)
        {
            if (!String.IsNullOrWhiteSpace(userId))
            {
                ApplicationUser user = RepositoryManager.UserManager.FindById(userId);

                if (user != null)
                {
                    user.IsBlocked = isBlocked;
                    RepositoryManager.UserManager.Update(user);
                }
            }
            return Index();
        } 
    }
}