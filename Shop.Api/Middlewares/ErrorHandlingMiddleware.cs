using Microsoft.AspNetCore.Http.HttpResults;
using Shop.Services.Exceptions;
using System.Text.Json;

namespace Shop.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
               await _next(context);

            }
            catch (Exception exc)
            {
                var responce = context.Response;
                responce.ContentType= "application/json";
                string message=exc.Message;
                var errors = new List<RestExceptionError>();

                switch (exc)
                {
                    case RestException restException:
                        responce.StatusCode=(int)restException.StatusCode;
                        message=restException.Message;
                        errors=restException.Errors;  
                        break;
                    
                    default:
                        responce.StatusCode = 500;
                        break;
                }
                var result = new { message = message,errors };
                await responce.WriteAsync(JsonSerializer.Serialize(result));

            }
        }

    }
}