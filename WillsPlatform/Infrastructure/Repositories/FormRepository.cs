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
    public class FormRepository : BaseRepository<Form>, IFormRepository
    {
        public FormRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
