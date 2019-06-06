using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static IQCare.SharedKernel.Infrastructure.Helpers.ConnectionStringBuilder;
using MediatR;
using AutoMapper;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.BusinessProcess.MapperProfiles;
using IQCare.SharedKernel.Infrastructure.Helpers;
using IQCare.Prep.Infrastructure.Installers;
using System.Reflection;

namespace IQCare.Prep.WebApi
{
    public class Startup
    {
        readonly string IQCareConnectionString = string.Empty;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IQCareConnectionString = BuildConnectionString(Configuration, new ConnectionString());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();
            services.AddPrepDbContext(IQCareConnectionString);
            services.AddMediatR(typeof(AddPrepStatusCommand).Assembly);
            services.AddAutoMapper(typeof(PrepStatusMapperProfile).Assembly);
            //   services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
