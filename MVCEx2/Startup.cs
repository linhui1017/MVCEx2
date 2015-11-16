using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCEx2.Startup))]
namespace MVCEx2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
