﻿using ClientServerMVCDemo.Services.ClientServices;
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
            Debug.WriteLine("page value = " + page);
            var viewModel = new ClientListViewModel();

            //await clientService.Create(new Data.Models.Client() { Name = "Stoqn", Description = "desc", Properties = null });
            //await clientService.Create(new Data.Models.Client() { Name = "Stoqn", Description = "desc", Properties = new Dictionary<string, string> { { "1", "Value1"} } });

            if(page == 0)
            {
                page = 1;
            }
            var pageData = await clientService.GetPage(page, 3);
            viewModel.Clients = pageData.ToList();
            viewModel.CurrentPage = pageData.PageIndex;
            viewModel.HasNextPage = pageData.HasNextPage;
            viewModel.HasPreviousPage = pageData.HasPreviousPage;

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

            if(!string.IsNullOrWhiteSpace(vm.NewPropertyName) && !string.IsNullOrWhiteSpace(vm.NewPropertyValue))
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

            if (!string.IsNullOrWhiteSpace(vm.NewPropertyName) && !string.IsNullOrWhiteSpace(vm.NewPropertyValue))
            {
                vm.Client.Properties.Add(vm.NewPropertyName, vm.NewPropertyValue);
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
