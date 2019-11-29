using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(your_Blog.Startup))]
namespace your_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
