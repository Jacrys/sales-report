using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sales_report.Startup))]
namespace sales_report
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
