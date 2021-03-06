﻿using System;
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
using IQCare.Pharm.BusinessProcess.Commands;
using IQCare.Pharm.Infrastructure;
using IQCare.SharedKernel.Infrastructure.Helpers;
using IQCare.Pharm.Infrastructure.Installers;
using IQCare.Pharm.BusinessProcess.Commands.Lookup;
using MediatR;
using IQCare.Pharm.BusinessProcess.Queries;

namespace IQCare.Pharm.WebApi
{
    public class Startup
    {
        public static string IQCareConnectionString;
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
            services.AddPharmDbContext(IQCareConnectionString);
            services.AddCors();
            services.AddMvc();
            services.AddMediatR(typeof(GetLookupFrequencyCommand).Assembly);

            services.AddMediatR(typeof(GetFacilityModuleQuery).Assembly);
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
