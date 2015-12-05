using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoTrader.Manager.Startup))]
namespace AutoTrader.Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
