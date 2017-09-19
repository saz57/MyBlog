using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

using MyBlog.DAL.Context;

namespace MyBlog.DAL.Repository
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(RoleStore<IdentityRole> store) : base(store)
        {
        }

        public static ApplicationRoleManager Create(BlogContext dbContext)
        {
            return  new ApplicationRoleManager(new RoleStore<IdentityRole>(dbContext));
        }
    }
}