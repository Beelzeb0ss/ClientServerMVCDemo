using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Utility;
using System.Linq.Expressions;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public interface IClientService
    {
        Task Create(Client client);
        Task Delete(int id);
        Task DeleteProperty(Client client, int indexInClient);
        Task<IEnumerable<Client>> Get(Expression<Func<Client, bool>> filter = null, string includeProperies = "");
        Task<Client> GetById(int id);
        Task<PaginatedList<Client>> GetPage(int pageIndex, int pageSize, string includeProperties = "");
        Task<List<Client>> SearchByName(string searchTerm, string includeProperties = "");
        Task Update(Client client);
    }
}