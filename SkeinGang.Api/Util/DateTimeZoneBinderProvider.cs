using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;

namespace SkeinGang.Api.Util;

internal class DateTimeZoneBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
        context.Metadata.ModelType == typeof(DateTimeZone)
            ? new DateTimeZoneBinder()
            : null;
}
