using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JokesWeb.Startup))]
namespace JokesWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
