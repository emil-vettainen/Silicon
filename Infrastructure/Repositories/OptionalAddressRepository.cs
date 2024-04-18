using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;

namespace Infrastructure.Repositories;

public class OptionalAddressRepository(AccountDbContext context) : BaseRepository<OptionalAddressEntity, AccountDbContext>(context)
{
    private readonly AccountDbContext _context = context;

}