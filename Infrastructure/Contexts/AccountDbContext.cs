using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : IdentityDbContext<AccountEntity>(options)
{
    
    public virtual DbSet<ProfileEntity> Profiles { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AccountEntity>()
            .HasOne(a => a.Profile)
            .WithOne(p => p.Account)
            .HasForeignKey<ProfileEntity>(p => p.UserId);

        builder.Entity<AccountEntity>()
            .HasMany(x => x.Addresses)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.UserId);
    }
}