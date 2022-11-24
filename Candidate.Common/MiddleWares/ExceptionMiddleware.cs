using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Candidate.Common.Core;
using Candidate.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace Candidate.Common.MiddleWares
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {


            var serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var exception = new
            {
                ex.Message,
                ex.StackTrace,
                ex.InnerException
            };

            var exceptionJson = JsonConvert.SerializeObject(exception, serializerSettings);

            context.Response.ContentType = "application/json";

            var detailedExceptionMessage = $"----------Exception---------{exceptionJson}---------";

            _logger.LogError($"{detailedExceptionMessage}");

            if (ex is BaseException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse() { Message = ex.Message }));
            }
            else if (ex is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                {
                    Message = "Unauthorized",
                    Status = HttpStatusCode.Unauthorized
                }));
            }
            else if (ex is DbUpdateException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if (ex.InnerException != null)
                {
                    var dbException = (SqlException)ex.InnerException;

                    switch (dbException.Number)
                    {
                        case 547:
                            {
                                var table = dbException.Message.Split("table");
                                var column = table[1].Split("column");
                                var error = new ErrorResponse
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = $"Wrong Foreign Key (Id) For Entity {column[0]}"
                                };

                                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                                break;
                            }
                        default:
                            {

                                var error = new ErrorResponse
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = dbException.Message
                                };
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                                break;
                            }
                    }
                }
                else
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse { Message = ex.Message }.ToString()));
                }


            }
            else
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                {
                    Message = _configuration["Enable_Stack_Trace"] == "TRUE" ? exceptionJson : ex.Message

                }));
            }
        }
    }
}


