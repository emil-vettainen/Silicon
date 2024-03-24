using Infrastructure.Entities.Api;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuthorEntity>()
            .HasMany(x => x.Courses)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId);

    }
}
