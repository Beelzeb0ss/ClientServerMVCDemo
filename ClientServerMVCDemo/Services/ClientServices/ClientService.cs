using ClientServerMVCDemo.Data.UnitOfWork;
using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Data.Utility;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ClientServerMVCDemo.Services.ClientServices
{
    public class ClientService : IClientService
    {
        private IClientServerUnitOfWork unitOfWork;

        public ClientService(IClientServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Client>> Get(Expression<Func<Client, bool>> filter = null, string includeProperies = "")
        {
            return await unitOfWork.ClientRepo.Get(filter, orderBy: q => q.OrderByDescending(x => x.Name), includeProperties: includeProperies);
        }

        public async Task<Client> GetById(int id)
        {
            return (await unitOfWork.ClientRepo.Get(x => x.Id == id, includeProperties:"Properties")).FirstOrDefault();
        }

        public async Task<PaginatedList<Client>> GetPage(int pageIndex, int pageSize, string includeProperties = "")
        {
            return await unitOfWork.ClientRepo.GetPage(pageIndex, pageSize, orderBy: x => x.Name, includeProperties: includeProperties);
        }

        public async Task<List<Client>> SearchByName(string searchTerm, string includeProperties = "")
        {
            Debug.WriteLine("Search term " + searchTerm);
            var results = (await unitOfWork.ClientRepo.Get(x => x.Name.Contains(searchTerm), includeProperties: includeProperties, orderBy: q => q.OrderByDescending(x => x.Name))).ToList();
            Debug.WriteLine("Search results " + results.Count);
            return results;
        }

        public async Task Create(Client client)
        {
            await unitOfWork.ClientRepo.Insert(client);
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

            //cascade does not work in-memory
            var props = await unitOfWork.ClientPropertyRepo.Get(x => x.ClientId == id);
            foreach (var prop in props)
            {
                unitOfWork.ClientPropertyRepo.Delete(prop);
            }
            var client = (await unitOfWork.ClientRepo.Get(x => x.Id == id, includeProperties: "FunctionPermissions")).FirstOrDefault();
            foreach (var p in client.FunctionPermissions)
            {
                unitOfWork.ClientFunctionPermissionRepo.Delete(p);
            }

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteProperty(Client client, int indexInClient)
        {
            var prop = client.Properties[indexInClient];
            client.Properties.RemoveAt(indexInClient);
            unitOfWork.ClientPropertyRepo.Delete(prop);

            await unitOfWork.SaveAsync();
        }
    }
}
