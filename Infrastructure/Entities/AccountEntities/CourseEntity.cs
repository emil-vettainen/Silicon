namespace Infrastructure.Entities.AccountEntities;

public class CourseEntity
{
    public string Id { get; set; } = null!;

    public ICollection<UserCourseEntity> Courses { get; set; } = [];
}
