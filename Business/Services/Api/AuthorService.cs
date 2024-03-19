using Business.Dtos.Author;
using Business.Factories;
using Infrastructure.Entities.Api;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Api;

namespace Business.Services.Api;

public class AuthorService
{
    private readonly AuthorRepository _authorRepository;

    public AuthorService(AuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<int> GetOrCreateAuthorAsync(AuthorDto dto)
    {

        try
        {
            var author = await _authorRepository.GetOneAsync(x => x.FullName == dto.FullName);
            if (author != null)
            {
                return author.Id;
            }
            else
            {
                var createdAuthor = await _authorRepository.CreateAsync(new AuthorEntity
                {
                    FullName = dto.FullName,
                    AuthorBiography = dto.AuthorBiography,
                    AuthorImageUrl = dto.AuthorImageUrl,
                    YoutubeUrl = dto.YoutubeUrl,
                    FollowersYoutube = dto.FollowersYoutube,
                    FacebookUrl = dto.FacebookUrl,
                    FollowersFacebook = dto.FollowersFacebook,
                });

                if (createdAuthor != null)
                {
                    return createdAuthor.Id;
                }
            }
        }
        catch (Exception)
        {

        }
        return 0;
    }
}


