using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Utility;

namespace ClientServerMVCDemo.Services.ServerServices
{
    public interface IServerService
    {
        Task Create(Server client);
        Task Delete(int id);
        Task DeleteFunction(Server server, int indexInServer);
        Task<Server> GetById(int id);
        Task<PaginatedList<Server>> GetPage(int pageIndex, int pageSize);
        Task Update(Server client);
    }
}