using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(obsproje555.Startup))]
namespace obsproje555
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
