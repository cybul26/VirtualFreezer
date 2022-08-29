using Microsoft.Extensions.Logging;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Infrastructure.EF;
using VirtualFreezer.Shared.Infrastructure.DAL;
using VirtualFreezer.Shared.Infrastructure.Security;

namespace VirtualFreezer.Identity.Infrastructure.DataInitializers;

internal class DatabaseDataInitializer : IDataInitializer
{
    private readonly IdentityDbContext _context;
    private readonly ILogger<DatabaseDataInitializer> _logger;
    private readonly IPasswordManager _passwordManager;

    public DatabaseDataInitializer(IdentityDbContext context, ILogger<DatabaseDataInitializer> logger,
        IPasswordManager passwordManager)
    {
        _context = context;
        _logger = logger;
        _passwordManager = passwordManager;
    }

    public async Task InitAsync()
    {
        if (_context.Users.Any())
        {
            return;
        }

        _logger.LogInformation("Creating administrator account...");
        var administrator =
            new User(Guid.NewGuid(), "admin@freezer.pl", _passwordManager.Secure("admin"), true);

        await _context.Users.AddAsync(administrator);
        await _context.SaveChangesAsync();
    }
}