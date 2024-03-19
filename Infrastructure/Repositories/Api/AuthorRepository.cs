using Infrastructure.Contexts;
using Infrastructure.Entities.Api;
using Shared.Utilis;

namespace Infrastructure.Repositories.Api;

public class AuthorRepository : BaseRepository<AuthorEntity, ApiDbContext>
{
    private readonly ApiDbContext _context;
    private readonly ErrorLogger _errorLogger;

    public AuthorRepository(ApiDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
    {
        _context = context;
        _errorLogger = errorLogger;
    }

}
