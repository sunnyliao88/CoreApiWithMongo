using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.MiddleWares;
using CoreApiWithMongo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters();
            
            // services.AddSingleton<IMongoDBSettings, MongoDBSettings>();
            MongoDBSettings mongoDBSettings = new MongoDBSettings();
            Configuration.Bind("MongoDBSettings", mongoDBSettings);
            services.AddSingleton(mongoDBSettings);
            //  IConfigurationSection conf=   Configuration.GetSection("test");

            services.AddSingleton<IBookServices, BookServices>();

            // services.AddSingleton<IDIDemo, DIDemoOracle>();
            services.AddSingleton<IDIDemo, DIDemoSQL>();
            // services.AddSingleton<IDIDemo, DIDemoOracle>();

            services.AddScoped<IEmployeeService, MockEmployeeService>();
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
                /*  ExceptionHandlerOptions options = new ExceptionHandlerOptions();
                  options.ExceptionHandlingPath = new PathString("/api/error");
                  app.UseExceptionHandler(options);

                  StatusCodePagesOptions statusCodePagesOptions = new StatusCodePagesOptions();
                  StatusCodeContext.
                  statusCodePagesOptions.HandleAsync =
                  app.UseStatusCodePages(context => context.HttpContext.)
               app.UseStatusCodePagesWithRedirects();
               */
            }


            /* app.Use(async (context, next) =>
             {
                 logger.LogInformation("*******************");
                 logger.Log(LogLevel.Warning, "this is log message for request f");
                 await next();
                 logger.Log(LogLevel.Error, "this is log message for response f");
             });
             */

            // app.UseFileServer();
            // app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMiddleware<MiddleWareDemo>();


            // app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id:int?}"
                    );

            });
            // app.UseMvc();


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

