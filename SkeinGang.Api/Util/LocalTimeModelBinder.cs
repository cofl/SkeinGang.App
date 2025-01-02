using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;

namespace SkeinGang.Api.Util;

internal class LocalTimeModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var rawData = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(rawData))
            bindingContext.Result = ModelBindingResult.Success(null);
        else if (TimeOnly.TryParse(rawData, out var time))
            bindingContext.Result = ModelBindingResult.Success(LocalTime.FromTimeOnly(time));
        else
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName,
                $"The value '{rawData}' is not a valid time. Use either 'HH:mm' or 'HH:mm:ss'.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
