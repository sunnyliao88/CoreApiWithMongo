using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiWithMongo.Controllers
{

    public class ErrorController : Controller
    {
        [Route("Exception")]
        public IActionResult ExceptionHandler()
        {
            ErrorVM error = new ErrorVM();
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            error.StatusCode = HttpContext.Response.StatusCode;
            error.Message = $"Unhandled error at {feature?.Path}, contact our support teadm at xxx please";
            
            return View("Error", error);
        }

        [Route("StatusCodePages/{code}")]
        public IActionResult StatusCodePagesHandler(int code)
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            ErrorVM error = new ErrorVM();
            error.StatusCode = code;
            string message = null;
            switch (error.StatusCode)
            {
                case 400:
                    message = $"Url: {feature?.OriginalPath}";
                    break;
                case 404:
                    message = $"Resource {feature?.OriginalPath}  NOT found";
                    break;
                case 500:
                    message = $"Unhandled error, contact our support teadm at xxx please";
                    break;
            }

            error.Message = message;
            return View("Error", error);
        }




    }
}
