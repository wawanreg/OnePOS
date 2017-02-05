using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnePOS.Startup))]
namespace OnePOS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
