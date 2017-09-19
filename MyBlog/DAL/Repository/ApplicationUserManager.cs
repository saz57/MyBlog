using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

using MyBlog.Models;
using MyBlog.DAL.Context;

namespace MyBlog.DAL.Repository
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(BlogContext dbContext)
        {
            ApplicationUser user = new ApplicationUser();
            return new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext));
        }
    }
}