﻿using Infrastructure.Contexts;
using Infrastructure.Entities.AccountEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class SavedCourseRepository(AccountDbContext context) : BaseRepository<SavedCourseEntity, AccountDbContext>(context)
{
    private readonly AccountDbContext _context = context;


    public async Task<IEnumerable<string>> GetSavedCoursesAsync(string userId)
    {
        try
        {
            return await _context.SavedCourses.Where(x => x.UserId == userId).Select(x => x.CourseId).ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<bool> DeleteAllSavedcoursesAsync(string userId)
    {
        try
        {
            var savedCourses = await _context.SavedCourses.Where(x => x.UserId == userId).ToListAsync();
            if (savedCourses.Count == 0)
            {
                return false;
            }
            _context.SavedCourses.RemoveRange(savedCourses);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


}