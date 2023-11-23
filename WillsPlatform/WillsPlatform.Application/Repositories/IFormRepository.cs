using Domains.Entities;

namespace WillsPlatform.Application.Repositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<List<Form>> GetAllFormDataAsync();
    }
}
