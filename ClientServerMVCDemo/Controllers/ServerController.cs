using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Services.ServerServices;
using ClientServerMVCDemo.ViewModels.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientServerMVCDemo.Controllers
{
    public class ServerController : Controller
    {
        private readonly IServerService serverService;

        public ServerController(IServerService serverService)
        {
            this.serverService = serverService;
        }

        public async Task<IActionResult> Index(int page)
        {
            var viewModel = new ServerListViewModel();

            if (page == 0)
            {
                page = 1;
            }
            var pageData = await serverService.GetPage(page, 3);

            viewModel.Servers = pageData.ToList();
            viewModel.CurrentPage = pageData.PageIndex;
            viewModel.HasNextPage = pageData.HasNextPage;
            viewModel.HasPreviousPage = pageData.HasPreviousPage;

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = new ServerEditViewModel();

            viewModel.Server = await serverService.GetById(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServerEditViewModel vm)
        {
            if(vm.Server.Functions == null)
            {
                vm.Server.Functions = new List<ServerFunction>();
            }

            for (int i = vm.Server.Functions.Count-1; i >= 0; i--)
            {
                if (vm.FunctionsToDelete[i])
                {
                    await serverService.DeleteFunction(vm.Server, i);
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.NewFunction))
            {
                vm.Server.Functions.Add(new ServerFunction { Name = vm.NewFunction});
            }

            await serverService.Update(vm.Server);

            return RedirectToAction("Edit", new { id = vm.Server.Id });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServerCreateViewModel vm)
        {
            if (vm.Server.Functions == null)
            {
                vm.Server.Functions = new List<ServerFunction>();
            }

            if (!string.IsNullOrWhiteSpace(vm.NewFunction))
            {
                vm.Server.Functions.Add(new ServerFunction { Name = vm.NewFunction });
            }

            await serverService.Create(vm.Server);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await serverService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
