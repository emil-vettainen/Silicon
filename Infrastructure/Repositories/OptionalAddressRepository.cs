using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Utilis;

namespace Infrastructure.Repositories;

public class OptionalAddressRepository : BaseRepository<OptionalAddressEntity, AccountDbContext>
{
    private readonly AccountDbContext _context;
    private readonly ErrorLogger _errorLogger;

    public OptionalAddressRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
    {
        _context = context;
        _errorLogger = errorLogger;
    }


}
