using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;

namespace SkeinGang.Api.Util;

internal class DateTimeZoneBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var rawData = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrEmpty(rawData))
            bindingContext.Result = ModelBindingResult.Success(null);
        else if (DateTimeZoneProviders.Tzdb.GetZoneOrNull(rawData) is { } zone)
            bindingContext.Result = ModelBindingResult.Success(zone);
        else
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName,
                $"The value '{rawData} is not a valid time zone. Refer to https://en.wikipedia.org/wiki/List_of_tz_database_time_zones");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
