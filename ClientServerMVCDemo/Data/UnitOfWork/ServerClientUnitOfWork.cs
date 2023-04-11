using ClientServerMVCDemo.Data.Repos;
using ClientServerMVCDemo.Data.Context;
using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Data.UnitOfWork
{
    public class ServerClientUnitOfWork : IServerClientUnitOfWork
    {
        private ClientServerDbContext context;
        private GenericRepository<Client> clientRepo;
        private GenericRepository<Server> serverRepo;

        public ServerClientUnitOfWork(ClientServerDbContext context)
        {
            this.context = context;
        }

        public GenericRepository<Client> ClientRepo
        {
            get
            {

                if (clientRepo == null)
                {
                    clientRepo = new GenericRepository<Client>(context);
                }
                return clientRepo;
            }
        }

        public GenericRepository<Server> ServerRepo
        {
            get
            {

                if (serverRepo == null)
                {
                    serverRepo = new GenericRepository<Server>(context);
                }
                return serverRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
