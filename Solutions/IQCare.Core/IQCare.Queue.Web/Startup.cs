using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using IQCare.Queue.Infrastructure.Installers;
using IQCare.SharedKernel.Infrastructure.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using MediatR;
using static IQCare.SharedKernel.Infrastructure.Helpers.ConnectionStringBuilder;
using IQCare.Queue.BusinessProcess.Command;
using IQCare.Queue.Infrastructure;
namespace IQCare.Queue.Web
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
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddQueueDbContext(IQCareConnectionString);
            services.AddMediatR(typeof(AddQueueCommand).Assembly);
           
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            app.UseMvc();
            
        }
    }
}
