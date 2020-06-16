using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreApiWithMongo.MiddleWares
{
    public class MiddleWareDemo
    {
        private RequestDelegate _next;
        public MiddleWareDemo(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            /* await  context.Response.WriteAsync("*** in middleWaredemo before");

               await _next(context);
               await context.Response.WriteAsync("*** in middleWaredemo after");
               */
            /*  await context.Response.WriteAsync(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
              await context.Response.WriteAsync("\n");

              string testHearder = context.Request.Headers["testHearder"];
              await context.Response.WriteAsync(string.IsNullOrEmpty(testHearder) ? "isnull" : testHearder);
              await context.Response.WriteAsync("\n");

              context.Request.Headers.Add("testHearder", "this is testHearder value");
              await context.Response.WriteAsync(context.Request.Headers["testHearder"]);
              await context.Response.WriteAsync("\n");  

              await context.Response.WriteAsync("this is response line1");
              await context.Response.WriteAsync("\n");
              */

            //  await context.Response.WriteAsync("this is response line1 before next");
            await _next(context);
            /* await context.Response.WriteAsync("this is response line1 after next");
             await context.Response.WriteAsync("\n");
             await context.Response.WriteAsync(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff tt"));
             await context.Response.WriteAsync("\n");
             */
        }
    }
}
