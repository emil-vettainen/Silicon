using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;

namespace Infrastructure.Repositories.SqlRepositories;

public class UserAddressRepository : SqlBaseRepository<UserAddressEntity, AccountDbContext>
{
    private readonly AccountDbContext _context;
    private readonly ErrorLogger _errorLogger;

    public UserAddressRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
    {
        _context = context;
        _errorLogger = errorLogger;
    }


    public async Task<UserAddressEntity> GetUserAddressAsync(string userId, int addressId)
    {
        try
        {
            var entity = await _context.UserAddresses.FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AddressId == addressId);
            if (entity != null)
            {
                return entity;
            }


        }
        catch (Exception)
        {


        }
        return null!;
    }



    public async Task<UserAddressEntity> GetAllAddressesAsync(string userId)
    {
        try
        {
            var entities = await _context.UserAddresses
                .Where(ua => ua.UserId == userId)
                .Include(a => a.Address)
                .Include(o => o.OptionalAddress)
                .FirstOrDefaultAsync();
            if (entities != null)
            {
                return entities;
            }
            return null!;
        }
        catch (Exception) { return null!; }

    }
}
