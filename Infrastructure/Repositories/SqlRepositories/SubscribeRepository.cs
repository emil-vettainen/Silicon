using Infrastructure.Contexts;
using Infrastructure.Entities.SubscribeEntities;
using Shared.Utilis;

namespace Infrastructure.Repositories.SqlRepositories
{
    public class SubscribeRepository : SqlBaseRepository<SubscribeEntity, AccountDbContext>
    {
        public SubscribeRepository(AccountDbContext context, ErrorLogger errorLogger) : base(context, errorLogger)
        {
        }
    }
}
