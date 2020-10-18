using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Library_Management.Startup))]
namespace Library_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
