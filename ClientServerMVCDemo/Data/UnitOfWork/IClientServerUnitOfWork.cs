using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Repos;

namespace ClientServerMVCDemo.Data.UnitOfWork
{
    public interface IClientServerUnitOfWork : IDisposable
    {
        GenericRepository<Client> ClientRepo { get; }
        GenericRepository<Server> ServerRepo { get; }
        void Save();
        Task SaveAsync();
    }
}