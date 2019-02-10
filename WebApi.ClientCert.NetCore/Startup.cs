using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.ClientCert.Demo.Authentication;
using WebApi.ClientCert.NetCore.Middleware;

namespace WebApi.ClientCert.NetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();                  
        }
              

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // NB: ensure that this is ordered first
            app.UseOwin(buildFunc =>
            {
                buildFunc.UseClientCertValidation(new DefaultClientCertificateValidator());
            });            

            app.UseMvc();
        }
    }
}
