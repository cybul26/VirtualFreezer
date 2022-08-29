using Microsoft.EntityFrameworkCore;

namespace VirtualFreezer.Shared.Testing;

public static class InMemoryDbHelper
{
    public static T CreateInMemoryContext<T>() where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = (T) Activator.CreateInstance(typeof(T), options)!;

        return context!;
    }
}