using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fan.Conveyancing.Web.Startup))]
namespace Fan.Conveyancing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
