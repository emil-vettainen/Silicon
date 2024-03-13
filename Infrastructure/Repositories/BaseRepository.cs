
using Microsoft.EntityFrameworkCore;
using Shared.Factories;
using Shared.Responses;
using Shared.Utilis;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity, TContext>(TContext context, ErrorLogger errorLogger) where TEntity : class where TContext : DbContext
{
    private readonly TContext _context = context;
    private readonly ErrorLogger _errorLogger = errorLogger;

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().AnyAsync(predicate);
            return entity;
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - ExistsAsync"); }
        return false;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) 
        {
            _errorLogger.ErrorLog(ex.Message, "BaseRepo - CreateAsync");
            return null!;
        } 
    }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - GetOneAsync"); }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            return entities;
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - GetAllAsync"); }
        return null!;
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null && updatedEntity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - UpdateAsync"); }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex) { _errorLogger.ErrorLog(ex.Message, "BaseRepo - DeleteAsync"); }
        return false;
    }
}