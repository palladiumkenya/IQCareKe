using IQCare.Common.BusinessProcess.Interfaces;
using IQCare.Common.BusinessProcess.Services;
using IQCare.HTS.BusinessProcess.Interfaces;
using IQCare.HTS.BusinessProcess.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace IQCare
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                .AddJsonOptions(o =>
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors();

            //Context
            services.AddDatabase(Configuration);
            services.AddCommonDatabase(Configuration);
            services.AddMediatR();

            //services.AddDbContext<HtsDbContext>(o => o.UseSqlServer(connectionString,x => x.MigrationsAssembly(typeof(HtsDbContext).GetTypeInfo().Assembly.GetName().Name)));
            //services.AddDbContext<CommonDbContext>(o => o.UseSqlServer(connectionString,x => x.MigrationsAssembly(typeof(CommonDbContext).GetTypeInfo().Assembly.GetName().Name)));

            //Repositories
            //services.AddScoped<ILookupItemViewRepository, LookupItemViewRepository>();

            //Services
            services.AddScoped<IHTSEncounterService, EncounterService>();
            services.AddScoped<ILookupItemViewService, LookupItemViewService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
