using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductsMVC.Startup))]
namespace ProductsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
