using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Utility;
using System.Linq.Expressions;

namespace ClientServerMVCDemo.Services.ServerServices
{
    public interface IServerService
    {
        Task Create(Server client);
        Task Delete(int id);
        Task DeleteFunction(Server server, int indexInServer);
        Task DeleteFunctionPermission(int id);
        Task<IEnumerable<Server>> Get(Expression<Func<Server, bool>> filter = null, string includeProperies = "");
        Task<Server> GetById(int id, string includeProperties = "");
        Task<PaginatedList<Server>> GetPage(int pageIndex, int pageSize, string includeProperties = "");
        Task Update(Server client);
    }
}