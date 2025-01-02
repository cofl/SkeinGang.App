using SkeinGang.Data.Context;

namespace SkeinGang.Api.Services;

public interface IHighlightService
{
    List<long> RefillCurrentlyHighlighted(DataContext context);
    void RemoveInvalidCurrentlyHighlighted(DataContext context);
    void RemoveAllCurrentlyHighlighted();
}
