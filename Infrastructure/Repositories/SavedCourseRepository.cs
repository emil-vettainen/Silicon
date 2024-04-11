using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntities;
using Infrastructure.Repositories.SqlRepositories;
using Microsoft.EntityFrameworkCore;
using Shared.Utilis;

namespace Infrastructure.Repositories
{
    public class SavedCourseRepository : SqlBaseRepository<SavedCourseEntity, AccountDbContext>
    {
        private readonly AccountDbContext _context;
        public SavedCourseRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
        {
            _context = context;
        }


        public async Task<IEnumerable<string>> GetSavedCoursesAsync(string userId)
        {
            try
            {
                return await _context.SavedCourses.Where(x => x.UserId == userId).Select(x => x.CourseId).ToListAsync();

            }
            catch (Exception)
            {

                return [];
            }
        }

        public async Task<bool> DeleteAllSavedcoursesAsync(string userId)
        {
            try
            {
                var savedCourses = await _context.SavedCourses.Where(x => x.UserId == userId).ToListAsync();
                if (!savedCourses.Any())
                {
                    return false;
                }

                _context.SavedCourses.RemoveRange(savedCourses);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //logger
                return false;
            }
        }

    }
}
