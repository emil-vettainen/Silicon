using Infrastructure.Entities.AccountEntites;

namespace Infrastructure.Entities.AccountEntities;

public class UserCourseEntity
{
    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public int CourseId { get; set; }
    public CourseEntity Course { get; set; } = null!;
}