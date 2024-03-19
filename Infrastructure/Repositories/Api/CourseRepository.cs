using Infrastructure.Contexts;
using Infrastructure.Entities.Api;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Api;

public class CourseRepository : BaseRepository<CourseEntity, ApiDbContext>
{
    private readonly ApiDbContext _context;
    private readonly ErrorLogger _errorLogger;

    public CourseRepository(ApiDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
    {
        _context = context;
        _errorLogger = errorLogger;

    }

    public override async Task<IEnumerable<CourseEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Courses.Include(x => x.Author).ToListAsync();
            return entities;
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - GetAllAsync"); }
        return null!;
    }

    public override async Task<CourseEntity> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Courses.Include(x => x.Author).FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - GetOneAsync"); }
        return null!;
    }
}
