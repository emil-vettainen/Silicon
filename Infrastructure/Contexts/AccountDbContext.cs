using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<UserAddressEntity> UserAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserAddressEntity>()
            .HasKey(ua => new { ua.UserId, ua.AddressId });

        builder.Entity<UserAddressEntity>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.UserAddresses)
            .HasForeignKey(u => u.UserId);

        builder.Entity<UserAddressEntity>()
            .HasOne(u => u.Address)
            .WithMany(a => a.UserAddresses)
            .HasForeignKey(ua => ua.AddressId);

    }
}