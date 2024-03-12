using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;

namespace Infrastructure.Repositories;

public class UserAddressRepository : BaseRepository<UserAddressEntity, AccountDbContext>
{
    private readonly AccountDbContext _context;
    private readonly ErrorLogger _errorLogger;

    public UserAddressRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
    {
        _context = context;
        _errorLogger = errorLogger;
    }



    public async Task<UserAddressEntity> GetAllAddressesAsync(string userId)
    {
        try
        {
            var entities = await _context.UserAddresses
                .Where(ua => ua.UserId == userId)
                .Include(a => a.Address)
                .FirstOrDefaultAsync();
            if (entities != null) 
            {
                return entities;
            }
            return null!;
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - GetAllAsync"); }
        return null!;
    }
}
