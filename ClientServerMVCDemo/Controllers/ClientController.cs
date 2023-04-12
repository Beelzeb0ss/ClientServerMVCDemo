using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Services.ClientServices;
using ClientServerMVCDemo.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientServerMVCDemo.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public async Task<IActionResult> Index(int page)
        {
            var viewModel = new ClientListViewModel();

            if(page <= 0)
            {
                page = 1;
            }
            var pageData = await clientService.GetPage(page, 3, "Properties");
            viewModel.Clients = pageData.ToList();
            viewModel.CurrentPage = pageData.PageIndex;
            viewModel.HasNextPage = pageData.HasNextPage;
            viewModel.HasPreviousPage = pageData.HasPreviousPage;

            foreach (var client in viewModel.Clients)
            {
                Debug.WriteLine("client id = " + client.Id.ToString());
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = new ClientEditViewModel();
            viewModel.Client = await clientService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClientEditViewModel vm)
        {
            if(vm.Client.Properties == null)
            {
                vm.Client.Properties = new List<ClientProperty>();
            }

            for (int i = vm.PropertiesToDelete.Count - 1; i >= 0; i--)
            {
                if (vm.PropertiesToDelete[i])
                {
                    await clientService.DeleteProperty(vm.Client, i);
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.NewPropertyName) && !string.IsNullOrWhiteSpace(vm.NewPropertyValue))
            {
                vm.Client.Properties.Add(new ClientProperty { Key = vm.NewPropertyName, Value = vm.NewPropertyValue });
            }

            await clientService.Update(vm.Client);

            return RedirectToAction("Edit", new { id = vm.Client.Id});
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateViewModel vm)
        {
            if(vm.Client.Properties == null)
            {
                vm.Client.Properties = new List<ClientProperty>();
            }

            if (!string.IsNullOrWhiteSpace(vm.NewPropertyName) && !string.IsNullOrWhiteSpace(vm.NewPropertyValue))
            {
                vm.Client.Properties.Add(new ClientProperty { Key = vm.NewPropertyName, Value = vm.NewPropertyValue });
            }

            await clientService.Create(vm.Client);
            return Redirect("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await clientService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
