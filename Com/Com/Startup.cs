using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Com.Startup))]
namespace Com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
