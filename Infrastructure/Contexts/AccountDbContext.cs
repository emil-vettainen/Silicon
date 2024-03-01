using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : IdentityDbContext<AccountEntity>(options)
{
    public virtual DbSet<AddressEntity> Address { get; set; }
    public virtual DbSet<ProfileEntity> Profile { get; set; }
    public virtual DbSet<SecondAddressEntity> SecondAddress { get; set; }
}