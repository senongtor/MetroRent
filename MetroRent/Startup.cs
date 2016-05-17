using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MetroRent.Startup))]
namespace MetroRent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
