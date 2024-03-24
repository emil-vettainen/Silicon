namespace Business.Dtos.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Ingress { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string? DiscountPrice { get; set; }
        public string Hours { get; set; } = null!;
        public string? LikesInNumbers { get; set; }
        public string? LikesInProcent { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsBestSeller { get; set; }
        public string? Articles { get; set; }
        public string? Resources { get; set; }
        public bool LifetimeAccess { get; set; }
        public bool Certificate { get; set; }
        public string FullName { get; set; } = null!;
        public string AuthorBiography { get; set; } = null!;
        public string? AuthorImageUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? FollowersYoutube { get; set; }
        public string? FacebookUrl { get; set; }
        public string? FollowersFacebook { get; set; }
    }
}
