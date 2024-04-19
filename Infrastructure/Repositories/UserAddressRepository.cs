using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntites;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class UserAddressRepository(AccountDbContext context) : BaseRepository<UserAddressEntity, AccountDbContext>(context)
{
    private readonly AccountDbContext _context = context;

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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message); 
            return null!; 
        }
    }
}