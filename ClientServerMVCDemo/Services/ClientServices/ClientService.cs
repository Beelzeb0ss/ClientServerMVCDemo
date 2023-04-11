using ClientServerMVCDemo.Data.UnitOfWork;
using ClientServerMVCDemo.Data.Models;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public class ClientService : IClientService
    {
        private IClientServerUnitOfWork unitOfWork;

        public ClientService(IClientServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Client>> GetPage(int pageIndex, int pageSize)
        {
            return await unitOfWork.ClientRepo.GetPage(pageIndex, pageSize, orderBy: x => x.Name);
        }

        public async void Create(Client client)
        {
            unitOfWork.ClientRepo.Insert(client);
            await unitOfWork.SaveAsync();
        }

        public async void Update(Client client)
        {
            unitOfWork.ClientRepo.Update(client);
            await unitOfWork.SaveAsync();
        }

        public async void Delete(Client client)
        {
            unitOfWork.ClientRepo.Delete(client);
            await unitOfWork.SaveAsync();
        }
    }
}
