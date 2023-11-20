using Domains.Entities;
using Infrastructure;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Repositories
{
    public sealed class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
