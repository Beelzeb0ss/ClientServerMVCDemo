using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Services.ServerServices
{
    public interface IServerService
    {
        void Create(Server client);
        void Delete(Server client);
        Task<IEnumerable<Server>> GetPage(int pageIndex, int pageSize);
        void Update(Server client);
    }
}