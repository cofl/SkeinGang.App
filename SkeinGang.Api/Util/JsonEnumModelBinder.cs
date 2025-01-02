using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SkeinGang.Api.Util;

internal class JsonEnumModelBinder(
    Type type,
    JsonSerializerOptions options,
    ILogger<JsonEnumModelBinder> logger
) : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var rawData = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        var data = JsonSerializer.Serialize(rawData);
        try
        {
            var result = JsonSerializer.Deserialize(data, type, options);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch (JsonException)
        {
            bindingContext.ModelState.AddModelError(bindingContext.BinderModelName ?? bindingContext.ModelName,
                $"The value '{rawData}' is not valid.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
