using Infrastructure.Entities.Api;

namespace Infrastructure.Data.Seed;

public class CourseSeeder
{
    public void Seed()
    {
        var authors = new List<AuthorEntity>
        {
            new AuthorEntity { FullName = "Robert Fox", AuthorImageUrl = "/images/uploads/3c9ac258-b543-4602-94c8-3eea7d29abe8.png", AuthorBiography = "", YoutubeUrl = "https://youtube.com", FollowersYoutube = "", FacebookUrl = "https://facebook.com", FollowersFacebook = "" }


        };
       
    }

   



   




}
