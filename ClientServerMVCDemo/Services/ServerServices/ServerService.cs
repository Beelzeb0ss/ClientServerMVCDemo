using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.UnitOfWork;
using ClientServerMVCDemo.Data.Utility;

namespace ClientServerMVCDemo.Services.ServerServices
{
    public class ServerService : IServerService
    {
        private IClientServerUnitOfWork unitOfWork;

        public ServerService(IClientServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //method to delete server function from db and remove from server
        public async Task<Server> GetById(int id, string includeProperties = "")
        {
            return (await unitOfWork.ServerRepo.Get(x => x.Id == id, includeProperties: includeProperties)).FirstOrDefault(); ;
        }

        public async Task<PaginatedList<Server>> GetPage(int pageIndex, int pageSize, string includeProperties = "")
        {
            return await unitOfWork.ServerRepo.GetPage(pageIndex, pageSize, orderBy: x => x.Name, includeProperties: includeProperties);
        }

        public async Task Create(Server client)
        {
            await unitOfWork.ServerRepo.Insert(client);
            await unitOfWork.SaveAsync();
        }

        public async Task Update(Server client)
        {
            unitOfWork.ServerRepo.Update(client);
            await unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            unitOfWork.ServerRepo.Delete(id);

            //cascade does not work in memory
            var funcs = await unitOfWork.ServerFunctionRepo.Get(x => x.ServerId == id, includeProperties: "Permissions");
            foreach (var func in funcs)
            {
                unitOfWork.ServerFunctionRepo.Delete(func);
                foreach(var p in func.Permissions)
                {
                    unitOfWork.ClientFunctionPermissionRepo.Delete(p);
                }
            }

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteFunction(Server server, int indexInServer)
        {
            var func = server.Functions[indexInServer];
            var permissions = await unitOfWork.ClientFunctionPermissionRepo.Get(x => x.Id == func.Id);

            //cascade ne raboti s inmemory bazata
            foreach(var p in permissions)
            {
                unitOfWork.ClientFunctionPermissionRepo.Delete(p);
            }
            server.Functions.RemoveAt(indexInServer);
            unitOfWork.ServerFunctionRepo.Delete(func);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteFunctionPermission(int id) 
        {
            unitOfWork.ClientFunctionPermissionRepo.Delete(id);
            await unitOfWork.SaveAsync();
        }
    }
}
