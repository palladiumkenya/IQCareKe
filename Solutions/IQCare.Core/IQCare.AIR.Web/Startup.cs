using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.AIR.Infrastructure.Installers;
using IQCare.SharedKernel.Infrastructure.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static IQCare.SharedKernel.Infrastructure.Helpers.ConnectionStringBuilder;

namespace IQCare.AIR.Web
{
    public class Startup
    {
        private static string IQCareConnectionString = string.Empty;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IQCareConnectionString = BuildConnectionString(Configuration, new ConnectionString());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAirDbContext(IQCareConnectionString);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
