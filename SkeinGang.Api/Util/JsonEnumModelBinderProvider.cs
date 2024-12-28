using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace SkeinGang.Api.Util;

internal class JsonEnumModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (!context.Metadata.ModelType.IsEnum)
            return null;

        var options = context.Services.GetRequiredService<IOptions<JsonOptions>>().Value.JsonSerializerOptions;
        var logger = context.Services.GetRequiredService<ILogger<JsonEnumModelBinder>>();
        return new JsonEnumModelBinder(context.Metadata.ModelType, options, logger);
    }
}
