using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using MyBlog.DAL.Context;
using MyBlog.Models;

namespace MyBlog
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole() { Name = "User" });
            roleManager.Create(new IdentityRole() { Name = "Admin" });
            context.SaveChanges();
        }
    }
}