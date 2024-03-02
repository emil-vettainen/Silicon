using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Utilis;

namespace Infrastructure.Repositories;

public class ProfileRepository(AccountDbContext context, ErrorLogger errorLogger) : BaseRepository<ProfileEntity, AccountDbContext>(context, errorLogger)
{





}