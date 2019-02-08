using System.Data.Entity;
using System.Web.Http;
using UsersDataWebAPI.Models;

namespace UsersDataWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new UserDBInitializer());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
