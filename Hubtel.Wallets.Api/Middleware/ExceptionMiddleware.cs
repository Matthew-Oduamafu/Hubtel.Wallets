using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExcetionAsync(httpContext, ex);
            }
        }

        private Task HandleExcetionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            string result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = ex.Message, ErrorType = "Failure", Source = ex.Source, TargetSite = ex.TargetSite.Name });

            switch (ex)
            {
                case BadRequestException badRequestException: statusCode = HttpStatusCode.BadRequest; break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Errors);
                    break;

                case NotFoundException notFoundException: statusCode = HttpStatusCode.NotFound; break;
                default: break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
    }
}