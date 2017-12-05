using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeHireApp.Startup))]
namespace BikeHireApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
