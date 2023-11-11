using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SolforbTest.WebClient.Models.ViewModels;

namespace SolforbTest.WebClient.Middlewares
{
    public class AppExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ViewResult> _viewExecutor;

        public AppExceptionHandlerMiddleware(
            RequestDelegate next,
            IActionResultExecutor<ViewResult> viewExecutor
        )
        {
            _next = next;
            _viewExecutor = viewExecutor;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string header = ex.GetType().Name;
            var viewResult = new ViewResult() { ViewName = "/Views/Shared/Error.cshtml", };
            var viewDataDictionary = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary()
            )
            {
                Model = new ErrorViewModel(header, ex.Message)
            };
            viewResult.ViewData = viewDataDictionary;

            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, new ActionDescriptor());

            await _viewExecutor.ExecuteAsync(actionContext, viewResult);
        }
    }

    public static class AppExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseAppExceptionHandler(
            this IApplicationBuilder builder
        ) => builder.UseMiddleware<AppExceptionHandlerMiddleware>();
    }
}
