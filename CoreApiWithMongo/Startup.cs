﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiWithMongo.AutoMap;
using CoreApiWithMongo.Data;
using CoreApiWithMongo.MiddleWares;
using CoreApiWithMongo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters();
            
            services.AddAutoMapper();

            //SqlDBSettings sqlDBSettings = new SqlDBSettings();
           // Configuration.Bind("SqlDBSettings", sqlDBSettings);
           // services.AddSingleton(sqlDBSettings);

            services.AddDbContextPool<AppDBContext>(
                options=>options.UseSqlServer(Configuration.GetConnectionString("CoreApiWithMongo")));
            // services.AddSingleton<IDBSettings, MongoDBSettings>();

           
            
            services.AddSingleton<IBookServices, BookServices>();
           // services.AddSingleton<IEmployeeService, MockEmployeeService>();
            services.AddScoped<IEmployeeService, SQLEmployeeService>();
            services.AddScoped<IDepartmentService, SQLDepartmentService>();

            //  services.AddSingleton(typeof(DemoService));
            services.AddSingleton<DemoService>();
          //  services.AddSingleton<DemoService, DemoService>();
           // services.AddSingleton(typeof(DemoService), new DemoService());
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Exception");
                app.UseStatusCodePagesWithReExecute("/StatusCodePages/{0}");               
            }

            app.UseStaticFiles();// app.UseFileServer(); // app.UseDefaultFiles();

            app.UseMiddleware<MiddleWareDemo>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id:int?}"
                    );

            });


            /*  app.Use(async (context, next) =>
              {
                  logger.LogInformation("*******************");
                  logger.Log(LogLevel.Warning, "this is log message for request");
                  await next();
                  logger.Log(LogLevel.Error, "this is log message for response");
              });

              app.Use(async (context, next) =>
              {
                  await context.Response.WriteAsync(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
                  await context.Response.WriteAsync("\n");

                  string testHearder = context.Request.Headers["testHearder"];
                  await context.Response.WriteAsync(string.IsNullOrEmpty(testHearder) ? "isnull" : testHearder);
                  await context.Response.WriteAsync("\n");

                  context.Request.Headers.Add("testHearder", "this is testHearder value");
                  await context.Response.WriteAsync(context.Request.Headers["testHearder"]);
                  await context.Response.WriteAsync("\n");

                  await context.Response.WriteAsync("this is response line1");
                  await context.Response.WriteAsync("\n");

                  await next();
                  await context.Response.WriteAsync("this is response line1 after next");
                  await context.Response.WriteAsync("\n");
                  await context.Response.WriteAsync(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
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
  */
        }

    }
}

