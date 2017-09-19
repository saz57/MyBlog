using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

using MyBlog.Models;

namespace MyBlog.DAL.Context
{
    public class BlogContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public static BlogContext Create()
        {
            return new BlogContext();
        }

        public BlogContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
    }
}