using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using MyBlog.Repository;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private static IRepository _repository;
        private List<Post> _posts;
        private int pageSize = 10;
        // GET: Home

        static HomeController()
        {
            _repository = new SQLRepository();
        }

        public ActionResult Index(int page = 1)
        {
            _posts = _repository.Get<Post>(page, pageSize);
            ViewBag.CurrentPage = page;
            if (_posts.Count >= pageSize)
            {
                ViewBag.HasNextPage = true;

            }

            ViewBag.Posts = _posts;//posts;
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            _repository.Put<Post>(post);
            ViewBag.Posts += post;
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult DeletePost(int? id)
        {
            if(id == 0)
            {
                return Redirect("Index");
            }

            _repository.DeleteById(Convert.ToInt32(id));
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult EditPost(int id = 0)
        {

             //       < a href = "/Home/EditPost/@b.PostId" > Редактировать </ a > < br />
            if (id == 0)
            {
                return Redirect("Index");
            }

            ViewBag.UpdatedPost = _repository.Get<Post>(id);
            return View();
            //return Redirect("EditPost");
        }

        [HttpPost]
        public ActionResult UpdatePost(Post post)
        {
            _repository.Update<Post>(post);
            return Redirect("Index");
        }
        
    }
}