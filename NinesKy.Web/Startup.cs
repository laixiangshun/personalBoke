using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NinesKy.Web.Startup))]
namespace NinesKy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
