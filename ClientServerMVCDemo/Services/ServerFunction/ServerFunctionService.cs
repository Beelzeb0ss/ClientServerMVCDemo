using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.UnitOfWork;

namespace ClientServerMVCDemo.Services.ServerFunction
{
    public class ServerFunctionService : IServerFunctionService
    {
        private IClientServerUnitOfWork unitOfWork;

        public ServerFunctionService(IClientServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Data.Models.ServerFunction> GetById(int id, string includeProperties = "")
        {
            return (await unitOfWork.ServerFunctionRepo.Get(x => x.Id == id, includeProperties: includeProperties)).FirstOrDefault();
        }

        public async Task<bool> DoesClientHaveAccess(Client client, Data.Models.ServerFunction function)
        {
            var result = await unitOfWork.ClientFunctionPermissionRepo.Get(x => x.ServerFunctionId == function.Id && x.ClientId == client.Id);
            if(result.FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DoesClientHaveAccess(int clientId, int functionId)
        {
            var result = await unitOfWork.ClientFunctionPermissionRepo.Get(x => x.ServerFunctionId == functionId && x.ClientId == clientId);
            if (result.FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }

        public async Task AddPermissionForClient(Client client, Data.Models.ServerFunction function)
        {
            await unitOfWork.ClientFunctionPermissionRepo.Insert(new ClientFunctionPermission { ClientId = client.Id, ServerFunctionId = function.Id });
            await unitOfWork.SaveAsync();
        }
    }
}
