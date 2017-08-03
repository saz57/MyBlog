using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using MyBlog;
using MyBlog.Repository;
using MyBlog.ViewModels;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private static PostRepository _postRepository;
        private static CommentRepository _commentRepository;
        private static UsersRepository _userRepisitory;

        private int pageSize = 10;

        static HomeController()
        {
            _postRepository = new PostRepository();
            _commentRepository = new CommentRepository();
            _userRepisitory = new UsersRepository();
        }

        public ActionResult Index(int page = 1)
        {
            HomeViewModel viewModel = new HomeViewModel();

            if (User.Identity.IsAuthenticated)
            {
                viewModel.CurrentUser = new CurrentUserViewModel(_userRepisitory.GetByNickName(User.Identity.Name));
            }

            viewModel.CurrentPage = page;
            viewModel.Posts = _postRepository.Get(page, pageSize);

            if (viewModel.Posts.Count() < pageSize)
            {
                viewModel.HasNextPage = false;
            }

            if (viewModel.Posts.Count() >= pageSize)
            {
                viewModel.HasNextPage = true;
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddPost(PostViewModel viewPost)
        {
            Post post = new Post();
            post.IsHidden = false;
            post.Autor = _userRepisitory.GetByNickName(User.Identity.Name,false);
            post.Content = viewPost.Content;
            _postRepository.Put(post);
            return Redirect("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(CommentViewModel viewComment)
        {
            Comment comment = new Comment();
            comment.IsHidden = false;
            comment.Post = _postRepository.Get(viewComment.PostId,false);
            comment.Autor = _userRepisitory.GetByNickName(User.Identity.Name,false);
            comment.Content = viewComment.Content;
            _commentRepository.Put(comment);
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeletePost(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            if (_postRepository.Get(id).Autor.NickName == User.Identity.Name)
            {
                _postRepository.DeleteById(id);
            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteComment(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            if (_commentRepository.Get(id).Autor.NickName == User.Identity.Name)
            {
                _commentRepository.DeleteById(id);
            }
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditPost(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            return View("EditPost", _postRepository.Get(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditComment(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            return View("EditComment", _commentRepository.Get(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdatePost(PostViewModel editedPost)
        {
            Post post = new Post();
            post.Id = editedPost.Id;
            post.Autor = _userRepisitory.GetById(editedPost.AutorId, false);
            post.Content = editedPost.Content;
            _postRepository.Update(post);
            return Redirect("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateComment(CommentViewModel editedComment)
        {
            Comment comment = new Comment();
            comment.Id = editedComment.Id;
            comment.Autor = _userRepisitory.GetById(editedComment.AutorId, false);
            comment.Post = _postRepository.Get(editedComment.PostId);
            comment.Content = editedComment.Content;
            _commentRepository.Update(comment);
            return Redirect("Index");
        }

        public ActionResult HidePost(int id)
        {
            _postRepository.Update(_postRepository.Get(id), true);
            return Redirect("Index");
        }

        public ActionResult HideComment(int id)
        {
            _commentRepository.Update(_commentRepository.Get(id), true);
            return Redirect("Index");
        }
    }
}