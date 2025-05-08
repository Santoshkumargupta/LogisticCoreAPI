using log4net;
using LogisticCore.Core.Exceptions;
using LogisticCore.Core.Model;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using AggregateException = LogisticCore.Core.Exceptions.AggregateException;
using NotImplementedException = LogisticCore.Core.Exceptions.NotImplementedException;
using UnauthorizedAccessException = LogisticCore.Core.Exceptions.UnauthorizedAccessException;

namespace LogisticCore.Core.Configurations
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static ILog _log = LogManager.GetLogger(typeof(GlobalErrorHandlingMiddleware));
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message;
            var exceptionResult = string.Empty;
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(AggregateException))
            {
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            _log.Error("Error Message: " + exception.Message);
            exceptionResult = JsonSerializer.Serialize(new ApiResponse<Object> { StatusCode = (int)status, ErrorMessage = message, Message = exception.InnerException != null ? exception.InnerException.ToString() : null }, serializeOptions);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
