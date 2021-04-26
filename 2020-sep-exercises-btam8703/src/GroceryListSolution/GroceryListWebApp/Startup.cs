using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroceryListWebApp.Startup))]
namespace GroceryListWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
