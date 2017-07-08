using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using MyBlog;
using MyBlog.Repository;


namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private static IRepository _repository;
        private int pageSize = 10;

        static HomeController()
        {
            _repository = new EntityRepository();
        }

        public ActionResult Index(int page = 1)
        {
            HomeViewModel viewModel = new HomeViewModel();
            List<Post> posts;
            posts = _repository.GetPosts(page, pageSize);
            viewModel.CurrentPage = page;
            viewModel.Posts = _repository.GetPosts(page, pageSize);
            viewModel.HasNextPage = false;

            if (posts.Count >= pageSize)
            {
                viewModel.HasNextPage = true;
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            _repository.Put(post);
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            _repository.Put(comment);
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult DeletePost(int? id)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            _repository.DeletePostById(Convert.ToInt32(id));
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult DeleteComment(int? id)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            _repository.DeleteCommentById(Convert.ToInt32(id));
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult EditPost(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            return View("EditPost", _repository.GetPost(id));
        }

        [HttpGet]
        public ActionResult EditComment(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            return View("EditComment", _repository.GetComment(id));
        }

        [HttpPost]
        public ActionResult UpdatePost(Post post)
        {
            _repository.Update(post);
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult UpdateComment(Comment comment)
        {
            _repository.Update(comment);
            return Redirect("Index");
        }

    }
}