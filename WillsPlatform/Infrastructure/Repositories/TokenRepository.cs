using Infrastructure;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
