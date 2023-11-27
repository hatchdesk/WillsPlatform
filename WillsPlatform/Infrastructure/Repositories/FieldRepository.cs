using Domains.Entities;
using Infrastructure;
using WillsPlatform.Application.Repositories;

namespace WillsPlatform.Infrastructure.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
