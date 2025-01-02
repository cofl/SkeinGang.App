using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;

namespace SkeinGang.Api.Util;

internal class LocalTimeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
        context.Metadata.ModelType == typeof(LocalTime?) ||
        context.Metadata.ModelType == typeof(LocalTime)
            ? new LocalTimeModelBinder()
            : null;
}
