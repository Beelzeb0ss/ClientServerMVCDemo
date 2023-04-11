using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public interface IClientService
    {
        Task Create(Client client);
        Task Delete(Client client);
        Task<Client> GetById(int id);
        Task<IEnumerable<Client>> GetPage(int pageIndex, int pageSize);
        Task Update(Client client);
    }
}