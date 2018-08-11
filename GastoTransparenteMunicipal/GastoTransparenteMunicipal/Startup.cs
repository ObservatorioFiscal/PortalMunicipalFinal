using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GastoTransparenteMunicipal.Startup))]
namespace GastoTransparenteMunicipal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
