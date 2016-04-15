using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MsgApp.Startup))]
namespace MsgApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
