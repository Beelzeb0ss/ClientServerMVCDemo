using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Repos;

namespace ClientServerMVCDemo.Data.UnitOfWork
{
    public interface IClientServerUnitOfWork : IDisposable
    {
        GenericRepository<Client> ClientRepo { get; }
        GenericRepository<Server> ServerRepo { get; }
        GenericRepository<ServerFunction> ServerFunctionRepo { get; }
        GenericRepository<ClientProperty> ClientPropertyRepo { get; }
        GenericRepository<ClientFunctionPermission> ClientFunctionPermissionRepo { get; }

        void Save();
        Task SaveAsync();
    }
}