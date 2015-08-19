using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LungmenSoftware.Startup))]
namespace LungmenSoftware
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
