using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSellWatch.Startup))]
namespace WebSellWatch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
