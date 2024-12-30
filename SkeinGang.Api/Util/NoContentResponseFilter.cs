using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SkeinGang.Api.Util;

/// <summary>
/// Change the status code for no-content actions to 204 No Content
/// (i.e., actions returning void or <see cref="Task"/>). 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
internal class NoContentResponseFilter : IResultFilter
{
    /// <inheritdoc />
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.ActionDescriptor is ControllerActionDescriptor { MethodInfo.ReturnType: var type }
            && (type == typeof(void) || type == typeof(Task)))
            context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
    }

    /// <inheritdoc />
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}