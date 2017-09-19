using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

using MyBlog.DAL.Context;
using MyBlog.DAL.Repository;


namespace MyBlog.DAL.RepositoriesManager
{
    public static class RepositoryManager
    {

        public static BlogContext DbContext { get; private set; }
        public static ApplicationUserManager UserManager { get; private set; }
        public static ApplicationRoleManager RoleManager { get; private set; }
        //public static ApplicationSignInManager SignInManager { get; private set; }
        public static PostRepository PostRepository { get; private set; }
        public static CommentRepository CommentRepository { get; private set; }
        public static PictureRepository PictureRepository { get; private set; }

        static RepositoryManager()
        {
            DbContext = BlogContext.Create();
            UserManager = ApplicationUserManager.Create(DbContext);
            RoleManager = ApplicationRoleManager.Create(DbContext);
            PictureRepository = new PictureRepository(DbContext);
            //SignInManager = ApplicationSignInManager.Create(_dbContext);
            PostRepository = new PostRepository(DbContext);
            CommentRepository = new CommentRepository(DbContext);
        }

    }
}