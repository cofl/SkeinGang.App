using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SkeinGang.Data.Context;

// ReSharper disable once ClassNeverInstantiated.Global
internal class EnumMemberToSnakeCaseStringConverter<TEnum>()
    : ValueConverter<TEnum, string>(
        v => Lookup.Names[v],
        v => Lookup.Values[v])
    where TEnum : struct, Enum
{
    private record EnumLookup(Dictionary<TEnum, string> Names, Dictionary<string, TEnum> Values);

    private static readonly EnumLookup Lookup = GetNames();

    private static EnumLookup GetNames()
    {
        var names = new Dictionary<TEnum, string>();
        var values = new Dictionary<string, TEnum>();
        foreach (var member in typeof(TEnum).GetEnumNames())
        {
            var value = Enum.Parse<TEnum>(member);
            var name = typeof(TEnum).GetMember(member).Single().GetCustomAttribute<EnumMemberAttribute>(false)?.Value ?? member.ToSnake();
            names.Add(value, name);
            values.Add(name, value);
        }
        
        return new EnumLookup(names, values);
    }
}