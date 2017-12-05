using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeApp.Startup))]
namespace BikeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
