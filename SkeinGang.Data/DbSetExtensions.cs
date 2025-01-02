using Microsoft.EntityFrameworkCore;

namespace SkeinGang.Data;

public static class DbSetExtensions
{
    public static T AddNew<T>(this DbSet<T> dbSet, T entity) where T : class
    {
        var proxy = dbSet.CreateProxy();
        dbSet.Entry(proxy).CurrentValues.SetValues(entity);
        dbSet.Add(proxy);
        return proxy;
    }
}
