using ClientServerMVCDemo.Services.ClientServices;
using ClientServerMVCDemo.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IActionResult> Index()
        {
            var viewModel = new ClientListViewModel();
            
            //await clientService.Create(new Data.Models.Client() { Name = "Stoqn", Description = "desc", Properties = null });
            //await clientService.Create(new Data.Models.Client() { Name = "Stoqn", Description = "desc", Properties = new Dictionary<string, string> { { "1", "Value1"} } });
            
            viewModel.Clients = await clientService.GetPage(1, 3);

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
                vm.Client.Properties = new Dictionary<string, string>();
            }

            if(!vm.NewPropertyName.IsNullOrEmpty() && !vm.NewPropertyValue.IsNullOrEmpty())
            {
                vm.Client.Properties.Add(vm.NewPropertyName, vm.NewPropertyValue);
            }

            foreach (var kvp in vm.PropertiesToDelete)
            {
                if (kvp.Value)
                {
                    vm.Client.Properties.Remove(kvp.Key);
                }
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
            if (vm.Client.Properties == null)
            {
                vm.Client.Properties = new Dictionary<string, string>();
            }

            if (!vm.NewPropertyName.IsNullOrEmpty() && !vm.NewPropertyValue.IsNullOrEmpty())
            {
                vm.Client.Properties.Add(vm.NewPropertyName, vm.NewPropertyValue);
            }

            await clientService.Create(vm.Client);
            return Redirect("Index");
        }
    }
}
