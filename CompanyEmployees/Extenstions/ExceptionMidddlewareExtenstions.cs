using Microsoft.AspNetCore.Builder;
using Contracts;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Entities.ErrorModel;

namespace CompanyEmployees.Extenstions
{
    public static class ExceptionMidddlewareExtenstions
    {
        public static void ConfigurExceptionHandler(this IApplicationBuilder app , ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature!=null)
                    {
                        logger.LogError($"Something went Wrong : {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            Message="Internal Server Error",
                            StatusCode= context.Response.StatusCode
                        }.ToString());
                    }
                });
            });
        }
    }
}
