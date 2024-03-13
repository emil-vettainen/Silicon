using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<OptionalAddressEntity> OptionalAddresses { get; set; }
    public DbSet<UserAddressEntity> UserAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserAddressEntity>()
            .HasKey(x => new { x.UserId, x.AddressId });

        builder.Entity<UserAddressEntity>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserAddresses)
            .HasForeignKey(x => x.UserId);

        builder.Entity<UserAddressEntity>()
            .HasOne(x => x.Address)
            .WithMany(x => x.UserAddresses)
            .HasForeignKey(x => x.AddressId);

        builder.Entity<UserAddressEntity>()
            .HasOne(x => x.OptionalAddress)
            .WithMany(x =>  x.UserAddresses)
            .HasForeignKey(x => x.OptionalAddressId)
            .IsRequired(false);

    }
}