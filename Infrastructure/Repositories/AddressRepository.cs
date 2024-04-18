using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;

namespace Infrastructure.Repositories
{
    public class AddressRepository(AccountDbContext context) : BaseRepository<AddressEntity, AccountDbContext>(context)
    {
        private readonly AccountDbContext _context = context;
    }
}