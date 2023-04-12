using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Utility;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public interface IClientService
    {
        Task Create(Client client);
        Task Delete(int id);
        Task DeleteProperty(Client client, int indexInClient);
        Task<Client> GetById(int id);
        Task<PaginatedList<Client>> GetPage(int pageIndex, int pageSize);
        Task Update(Client client);
    }
}