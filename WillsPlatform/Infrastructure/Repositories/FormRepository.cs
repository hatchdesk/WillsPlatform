using Domains.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillsPlatform.Application.Repositories;

namespace WillsPlatform.Infrastructure.Repositories
{
    public class FormRepository : BaseRepository<Form>,IFormRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FormRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Form>> GetAllFormDataAsync()
        {
            return (await GetAllAsync()).ToList();
        }
    }
}
