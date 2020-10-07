using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MalaGroupERP.Startup))]
namespace MalaGroupERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
