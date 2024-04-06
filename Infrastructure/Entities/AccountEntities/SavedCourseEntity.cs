using Infrastructure.Entities.AccountEntites;

namespace Infrastructure.Entities.AccountEntities;

public class SavedCourseEntity
{
    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public string CourseId { get; set; } = null!;
}