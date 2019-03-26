using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MultiwarehouseMgt.Startup))]
namespace MultiwarehouseMgt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
