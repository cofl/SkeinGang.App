using System.Collections;

namespace SkeinGang.Api.Util;

internal class NonNullList<T> : IEnumerable<T>
{
    private readonly List<T> _list = [];
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => _list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

    public void Add(T? item)
    {
        if (item != null)
            _list.Add(item);
    }

    public List<T> ToList() => _list;
}
