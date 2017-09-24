using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;

using MyBlog.DAL.Context;

namespace MyBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<BlogContext>(new ContextInitializer());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
