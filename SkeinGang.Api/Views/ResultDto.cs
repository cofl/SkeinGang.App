namespace SkeinGang.Api.Views;

public record ResultDto<T>(bool HasMore, List<T> Data);