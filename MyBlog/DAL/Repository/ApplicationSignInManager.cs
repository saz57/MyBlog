using System.Web.Http.Owin;
using System.Web;
using System.Net;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using MyBlog.Models;
using MyBlog.DAL.RepositoriesManager;


namespace MyBlog.DAL.Repository
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
            
        }

        public static ApplicationSignInManager Create()
        {
            ApplicationUserManager userManager = ApplicationUserManager.Create(RepositoryManager.DbContext);
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            ApplicationSignInManager signInManager = new ApplicationSignInManager(userManager,authenticationManager);
            return signInManager;
        }
    }
}