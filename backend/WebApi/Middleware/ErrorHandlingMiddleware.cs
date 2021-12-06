using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using Services.Exceptions;

namespace WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException ex)
            {
                await HandleBusinessExceptionAsyn(context, ex);
            }
            catch (Exception ex)
            {
                await LogErrorExceptionWithRequestBody(context, ex);
                await HandleUnhandledExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync
        (
            HttpContext context,
            Exception exception,
            int status,
            string errorMessage,
            bool showWholeStackTrace
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            if (showWholeStackTrace && _environment.EnvironmentName.Equals("Development"))
            {
                errorMessage = exception.ToString();
            }

            string errorMessageAsJson = JsonConvert.SerializeObject(new
            {
                Message = errorMessage
            }, Formatting.Indented);

            await context.Response.WriteAsync(errorMessageAsJson);
        }

        private async Task HandleBusinessExceptionAsyn(HttpContext context, BaseException exception)
        {
            await HandleExceptionAsync
            (
                context,
                exception,
                (int)HttpStatusCode.Conflict,
                exception.Message,
                false
            );
        }

        private async Task HandleUnhandledExceptionAsync(HttpContext context, Exception exception)
        {
            await HandleExceptionAsync
            (
                context,
                exception,
                (int)HttpStatusCode.InternalServerError,
                "Internal server error happened. Please contact support",
                true
            );
        }

        private async Task LogErrorExceptionWithRequestBody(HttpContext context, Exception exception)
        {
            context.Request.EnableBuffering();
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                string body = await reader.ReadToEndAsync();

                _logger.Error(
                    exception,
                    $"WebApi exception, Method: {{method}}, Content: {{faultMessage}}",
                    $"{context.Request.Method} {context.Request.GetDisplayUrl()}",
                    JsonConvert.SerializeObject(body)
                );

                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}