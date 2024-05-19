using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartManagerLibrarySystem.Startup))]
namespace SmartManagerLibrarySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
