using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppOMST.Startup))]
namespace WebAppOMST
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
