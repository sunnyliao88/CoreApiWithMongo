using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreApiWithMongo
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // services.AddSingleton<IMongoDBSettings, MongoDBSettings>();
            MongoDBSettings mongoDBSettings = new MongoDBSettings();
            Configuration.Bind("MongoDBSettings", mongoDBSettings);
            services.AddSingleton(mongoDBSettings);
       //  IConfigurationSection conf=   Configuration.GetSection("test");
            
            services.AddSingleton<IBookServices, BookServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseFileServer();
            // app.UseStaticFiles();
            // app.UseDefaultFiles();

            // app.UseMvc();

            app.Use(async (context,next) =>
            {               
                string testHearder = context.Request.Headers["testHearder"];
                await context.Response.WriteAsync(string.IsNullOrEmpty(testHearder) ? "isnull": testHearder);
                await context.Response.WriteAsync("\n");

                context.Request.Headers.Add("testHearder", "this is testHearder value");
                await context.Response.WriteAsync(context.Request.Headers["testHearder"]);
                await context.Response.WriteAsync("\n");

                await context.Response.WriteAsync("this is response line1");
                await context.Response.WriteAsync("\n");

                await  next();
                await context.Response.WriteAsync("this is response line1 after next");
                await context.Response.WriteAsync("\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync(context.Request.Headers["testHearder"]);
                await context.Response.WriteAsync("\n");

                await context.Response.WriteAsync("this is response line2");
                await context.Response.WriteAsync("\n");

                await next();
                await context.Response.WriteAsync("this is response line2 after next");
                await context.Response.WriteAsync("\n");
            });           

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                await context.Response.WriteAsync("\n");
            });
        }


    }
}

