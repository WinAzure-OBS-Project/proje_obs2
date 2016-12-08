using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proje_obs.Startup))]
namespace proje_obs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
