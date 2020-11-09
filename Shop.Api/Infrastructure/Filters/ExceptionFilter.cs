using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shop.Api.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IHostEnvironment env;
        private readonly ILogger logger;

        public ExceptionFilter(IHostEnvironment env, ILogger<ExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var content = new Dictionary<string, object>
                {
                    { "ErrorMessage", context.Exception.Message }
                };

                if (env.IsDevelopment())
                {
                    content.Add("Exception", context.Exception.StackTrace);

                    return;
                }

                var statusCode = (int)MapStatusCode(context.Exception);

                LogError(context, statusCode);

                context.Result = new ObjectResult(content);
                context.HttpContext.Response.StatusCode = statusCode;
                context.Exception = null;
            }
        }

        private void LogError(ExceptionContext context, int statusCode)
        {
            var logTitle =
                    $"{context.HttpContext.Request.Path} :: [{statusCode}] {context.Exception.Message}";
            var logError = new
            {
                Context = context
            };

            if (statusCode >= 500)
            {
                logger.LogCritical(logTitle, logError);
            }
            else if (statusCode == 404 || statusCode == 401)
            {
                logger.LogInformation(logTitle, logError);
            }
            else
            {
                logger.LogWarning(logTitle, logError);
            }
        }

        private HttpStatusCode MapStatusCode(Exception ex)
        {
            switch (ex)
            {
                // Status Codes
                case ArgumentNullException _:
                    return HttpStatusCode.NotFound;
                case ValidationException _:
                    return HttpStatusCode.BadRequest;
                case UnauthorizedAccessException _:
                    return HttpStatusCode.Unauthorized;
                case DuplicateNameException _:
                    return HttpStatusCode.Conflict;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}