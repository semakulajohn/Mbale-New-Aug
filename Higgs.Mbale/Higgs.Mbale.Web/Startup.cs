using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Higgs.Mbale.Web.Startup))]
namespace Higgs.Mbale.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
