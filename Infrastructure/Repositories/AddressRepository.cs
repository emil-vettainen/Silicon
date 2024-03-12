using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity, AccountDbContext>
    {
        private readonly AccountDbContext _context;
        public AddressRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
        {
            _context = context;
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


        public async Task AddUserAddressAsync(UserAddressEntity userAddress)
        {
            _context.UserAddresses.Add(userAddress);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateUserAddressAsync(UserAddressEntity userAddress)
        {
           _context.UserAddresses.Update(userAddress);
            await _context.SaveChangesAsync();
        }

   
    }
}
