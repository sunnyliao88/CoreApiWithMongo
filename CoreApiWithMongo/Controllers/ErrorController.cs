using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApiWithMongo.Controllers
{

    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Exception")]
        public IActionResult ExceptionHandler()
        {
            ErrorVM error = new ErrorVM();
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            error.StatusCode = HttpContext.Response.StatusCode;
            error.Message = $"An exception occured while processing your request at {feature?.Path}. The support team is notified";
            _logger.LogError(feature.Error.GetType().Name);
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
                    message = $"An exception occured while processing your request at {feature?.OriginalPath}. The support team is notified";
                    break;
            }

            error.Message = message;
            return View("Error", error);
        }




    }
}
