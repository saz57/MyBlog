using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyBlog.Models;

namespace MyBlog
{
    public class BlogContext :DbContext
    {
        public DbSet<ApplicationUser> UsersSet { get; set; }
        public DbSet<Post> PostsSet { get; set; }
        public DbSet<Comment> CommentsSet { get; set; }
        public RoleManager<IdentityRole> RoleManager;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

        }

        public BlogContext() : base("DefaultConnection")
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this));
        }
    }
}