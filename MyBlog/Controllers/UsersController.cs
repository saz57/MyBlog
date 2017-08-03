using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Repository;
using MyBlog.Models;
using MyBlog.Enums;
using MyBlog.ViewModels;

namespace MyBlog.Controllers
{
    public class UsersController : Controller
    {
        private static UsersRepository _userRepository;
        private int pageSize = 20;

        static UsersController()
        {
            _userRepository = new UsersRepository();
        }

        public ActionResult Index(int currentPage = 1)
        {
            UsersListViewModel viewModel = new UsersListViewModel();
            if(User.Identity.IsAuthenticated)
            {
                viewModel.CurrentUser = new CurrentUserViewModel(_userRepository.GetByNickName(User.Identity.Name));
            }

            viewModel.Users = _userRepository.Get(currentPage, pageSize);
            viewModel.CurrentPage = currentPage;
            
            if (viewModel.Users.Count() < pageSize)
            {
                viewModel.HasNextPage = false;
            }

            if (viewModel.Users.Count() >= pageSize)
            {
                viewModel.HasNextPage = true;
            }

            return View("Index", viewModel);
        }
        
        [HttpGet]
        public ActionResult ShowUserProfile(int id = 0)
        {
            if (id != 0)
            {
                UserViewModel viewModel = new UserViewModel(_userRepository.GetById(id));
                viewModel.CurrentUser = new CurrentUserViewModel(_userRepository.GetByNickName(User.Identity.Name));
                return View("UserView", viewModel);
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeRole(int id = 0)
        {
            if (id != 0)
            {
                ApplicationUser user = _userRepository.GetById(id, false);
                _userRepository.Update(user,true);
            }
            return View();
        }
    }
}