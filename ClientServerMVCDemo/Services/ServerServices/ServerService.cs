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

        public async Task<Server> GetById(int id)
        {
            return await unitOfWork.ServerRepo.GetByID(id);
        }

        public async Task<PaginatedList<Server>> GetPage(int pageIndex, int pageSize)
        {
            return await unitOfWork.ServerRepo.GetPage(pageIndex, pageSize, orderBy: x => x.Name);
        }

        public async Task Create(Server client)
        {
            unitOfWork.ServerRepo.Insert(client);
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
            await unitOfWork.SaveAsync();
        }
    }
}
