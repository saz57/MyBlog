using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

using MyBlog.Models;
using MyBlog.DAL.RepositoriesManager;
using MyBlog.ViewModels;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {

        private int pageSize = 10;

        public ActionResult Index(int page = 1)
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.CurrentPage = page;
            viewModel.Posts = new PagedList<Post>(RepositoryManager.PostRepository.Get(page, pageSize), page, pageSize);

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
        #region Post

        [HttpGet]
        [Authorize]
        public ActionResult AddPost()
        {
            return View("AddPost",new PostViewModel());
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddPostAjax()
        {
            string postName = Request["postName"];
            string postContent = Request["postContent"];

            if (!String.IsNullOrWhiteSpace(postName) && !String.IsNullOrWhiteSpace(postContent))
            {
                Post post = new Post();
                post.IsHidden = false;
                post.Autor = RepositoryManager.UserManager.FindById(User.Identity.GetUserId());
                post.Name = postName;
                post.Content = postContent;
                post = RepositoryManager.PostRepository.Put(post);
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase upload = Request.Files[file];

                    if (upload != null)
                    {
                        Picture picture = RepositoryManager.PictureRepository.Put(upload,post);
                    }
                }

                return Redirect("Index");

            }

            return View("AddPost");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditPost(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            Post post = RepositoryManager.PostRepository.Get(id);

            if (post == null)
            {
                return Redirect("Index");
            }

            return View("EditPost", RepositoryManager.PostRepository.Get(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdatePost(PostViewModel editedPost)
        {
            Post post = new Post();
            post.Id = editedPost.Id;
            post.Name = editedPost.Name;
            post.Content = editedPost.Content;
            RepositoryManager.PostRepository.Update(post);
            return Redirect("Index");
        }

        public ActionResult EditPostAjax()
        {
            int id = 0;

            if (!Int32.TryParse(Request["postId"], out id))
            {
                return Redirect("Index");
            }

            string postName = Request["postName"];
            string postContent = Request["postContent"];
            


            if (!String.IsNullOrWhiteSpace(postName) && !String.IsNullOrWhiteSpace(postContent))
            {

                Post post = RepositoryManager.PostRepository.Get(id);
                post.IsHidden = false;
                post.Name = postName;
                post.Content = postContent;
                List<string> imagesToDelete = System.Web.Helpers.Json.Decode<List<string>>(Request["imagesToDelete"]);

                if (imagesToDelete != null)
                {
                    foreach (string stirngId in imagesToDelete)
                    {
                        int i;
                        if (Int32.TryParse(stirngId, out i))
                        {
                            RepositoryManager.PictureRepository.DeleteById(i);
                        }
                    }
                }
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase upload = Request.Files[file];

                    if (upload != null)
                    {
                        Picture picture = RepositoryManager.PictureRepository.Put(upload, post);
                    }
                }

                RepositoryManager.PostRepository.Update(post);

                return Redirect("Index");

            }

            return View("EditPost");
        }
    

        [HttpGet]
        [Authorize]
        public ActionResult DeletePost(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            Post post = RepositoryManager.PostRepository.Get(id);

            if (post != null && post.Autor.UserName == User.Identity.Name)
            {
                RepositoryManager.PostRepository.DeleteById(id);
            }

            return Redirect("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SetIsHidePost(int id, bool hide)
        {
            Post post = RepositoryManager.PostRepository.Get(id);

            if (post != null)
            {
                RepositoryManager.PostRepository.Update(post, hide);
            }

            return Redirect("Index");
        }


        #endregion
        #region Comment
        [HttpPost]
        public ActionResult ViewComments(Post post)
        {
            return PartialView("CommentsPartial", post.Comments);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(CommentViewModel viewComment)
        {
            Comment comment = new Comment();
            comment.IsHidden = false;
            comment.Post = RepositoryManager.PostRepository.Get(viewComment.PostId, false);
            comment.Autor = RepositoryManager.UserManager.FindById(User.Identity.GetUserId());
            comment.Content = viewComment.Content;
            RepositoryManager.CommentRepository.Put(comment);
            return Redirect("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditComment(int id = 0)
        {
            if (id <= 0)
            {
                return Redirect("Index");
            }

            Comment comment = RepositoryManager.CommentRepository.Get(id);

            if (comment == null)
            {
                return Redirect("Index");
            }
            return View("EditComment", RepositoryManager.CommentRepository.Get(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateComment(CommentViewModel editedComment)
        {
            Comment comment = new Comment();
            comment.Id = editedComment.Id;
            comment.Post = RepositoryManager.PostRepository.Get(editedComment.PostId);
            comment.Content = editedComment.Content;
            RepositoryManager.CommentRepository.Update(comment);
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

            Comment comment = RepositoryManager.CommentRepository.Get(id);

            if (comment != null && comment.Autor.UserName == User.Identity.Name)
            {
                RepositoryManager.CommentRepository.DeleteById(id);
            }

            return Redirect("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SetIsHideComment(int id, bool hide)
        {
            Comment comment = RepositoryManager.CommentRepository.Get(id);

            if (comment != null)
            {
                RepositoryManager.CommentRepository.Update(RepositoryManager.CommentRepository.Get(id), hide);
            }

            return Redirect("Index");
        }
        #endregion
    }
}