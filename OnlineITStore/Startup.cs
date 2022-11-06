using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineITStore.Startup))]
namespace OnlineITStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
