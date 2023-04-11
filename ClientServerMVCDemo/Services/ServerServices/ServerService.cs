using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.UnitOfWork;

namespace ClientServerMVCDemo.Services.ServerServices
{
    public class ServerService : IServerService
    {
        private IClientServerUnitOfWork unitOfWork;

        public ServerService(IClientServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Server>> GetPage(int pageIndex, int pageSize)
        {
            return await unitOfWork.ServerRepo.GetPage(pageIndex, pageSize, orderBy: x => x.Name);
        }

        public async void Create(Server client)
        {
            unitOfWork.ServerRepo.Insert(client);
            await unitOfWork.SaveAsync();
        }

        public async void Update(Server client)
        {
            unitOfWork.ServerRepo.Update(client);
            await unitOfWork.SaveAsync();
        }

        public async void Delete(Server client)
        {
            unitOfWork.ServerRepo.Delete(client);
            await unitOfWork.SaveAsync();
        }
    }
}
