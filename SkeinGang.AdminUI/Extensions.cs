using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI;

internal static class Extensions
{
    internal static StatusCodeResult SeeOther(this Controller controller, string location)
    {
        controller.Response.Headers.Append("Location", location);
        return new StatusCodeResult(StatusCodes.Status303SeeOther);
    }
}
