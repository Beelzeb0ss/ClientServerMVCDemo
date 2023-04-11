using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public interface IClientService
    {
        void Create(Client client);
        void Delete(Client client);
        Task<IEnumerable<Client>> GetPage(int pageIndex, int pageSize);
        void Update(Client client);
    }
}