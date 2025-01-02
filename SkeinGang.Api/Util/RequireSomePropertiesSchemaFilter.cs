using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using SkeinGang.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SkeinGang.Api.Util;

// ReSharper disable once ClassNeverInstantiated.Global
internal class RequireSomePropertiesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var isDefaultRequired = context.Type.GetCustomAttribute<DefaultRequiredAttribute>() is not null;
        var ignoredProperties = isDefaultRequired
            ? context.Type.GetProperties()
                .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>(true) is not null)
                .ToDictionary(p => p.Name.ToSnake(), p => p.GetCustomAttribute<JsonIgnoreAttribute>()!.Condition)
            : null;

        foreach (var (key, value) in schema.Properties.ToList())
        {
            if (schema.Required.Contains(key)) continue;
            if (ignoredProperties?.TryGetValue(key.ToSnake(), out var condition) ?? false)
                switch (condition)
                {
                    case JsonIgnoreCondition.Always:
                        schema.Properties.Remove(key);
                        continue;
                    case JsonIgnoreCondition.Never:
                        schema.Required.Add(key);
                        continue;
                    default:
                        continue;
                }

            if (isDefaultRequired)
                schema.Required.Add(key);
        }
    }
}
