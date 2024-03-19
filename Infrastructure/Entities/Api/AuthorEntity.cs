namespace Infrastructure.Entities.Api
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string AuthorBiography { get; set; } = null!;
        public string? AuthorImageUrl { get; set; }
        public string? YoutubeUrl { get; set; } 
        public string? FollowersYoutube {  get; set; }
        public string? FacebookUrl { get; set; }
        public string? FollowersFacebook { get; set; }
        public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();
    }
}
