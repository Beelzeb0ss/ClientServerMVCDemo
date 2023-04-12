using ClientServerMVCDemo.Data.Repos;
using ClientServerMVCDemo.Data.Context;
using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Data.UnitOfWork
{
    public class ClientServerUnitOfWork : IClientServerUnitOfWork
    {
        private ClientServerDbContext context;
        private GenericRepository<Client> clientRepo;
        private GenericRepository<Server> serverRepo;
        private GenericRepository<ClientProperty> clientPropertyRepo;
        private GenericRepository<ServerFunction> serverFunctionRepo;

        public ClientServerUnitOfWork(ClientServerDbContext context)
        {
            this.context = context;
            context.Database.EnsureCreated(); //move to startup/program/kvoto e sq
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

        public GenericRepository<ClientProperty> ClientPropertyRepo
        {
            get
            {
                if(clientPropertyRepo == null)
                {
                    clientPropertyRepo = new GenericRepository<ClientProperty>(context);
                }
                return clientPropertyRepo;
            }
        }

        public GenericRepository<ServerFunction> ServerFunctionRepo
        {
            get
            {
                if (serverFunctionRepo == null)
                {
                    serverFunctionRepo= new GenericRepository<ServerFunction>(context);
                }
                return serverFunctionRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
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
