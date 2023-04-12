using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Services.ServerFunction
{
    public interface IServerFunctionService
    {
        Task AddPermissionForClient(Client client, Data.Models.ServerFunction function);
        Task<bool> DoesClientHaveAccess(Client client, Data.Models.ServerFunction function);
        Task<Data.Models.ServerFunction> GetById(int id, string includeProperties = "");
    }
}