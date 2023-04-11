using ClientServerMVCDemo.Data.UnitOfWork;
using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Utility;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public class ClientService : IClientService
    {
        private IClientServerUnitOfWork unitOfWork;

        public ClientService(IClientServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Client> GetById(int id)
        {
            return await unitOfWork.ClientRepo.GetByID(id);
        }

        public async Task<PaginatedList<Client>> GetPage(int pageIndex, int pageSize)
        {
            return await unitOfWork.ClientRepo.GetPage(pageIndex, pageSize, orderBy: x => x.Name);
        }

        public async Task Create(Client client)
        {
            unitOfWork.ClientRepo.Insert(client);
            await unitOfWork.SaveAsync();
        }

        public async Task Update(Client client)
        {
            unitOfWork.ClientRepo.Update(client);
            await unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            unitOfWork.ClientRepo.Delete(id);
            await unitOfWork.SaveAsync();
        }
    }
}
