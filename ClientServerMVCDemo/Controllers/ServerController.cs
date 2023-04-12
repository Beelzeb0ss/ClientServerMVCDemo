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
            Debug.WriteLine("page value = " + page);
            var viewModel = new ServerListViewModel();

            //await serverService.Create(new Data.Models.Server() { Name = "Serv1", Description = "desc1", Functions = new List<string>() { "func1", "func2" } });
            //await serverService.Create(new Data.Models.Server() { Name = "Serv2", Description = "desc2" });

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
            if (vm.Server.Functions == null)
            {
                vm.Server.Functions = new List<string>();
            }

            for (int i = vm.Server.Functions.Count-1; i >= 0; i--)
            {
                if (vm.FunctionsToDelete[i])
                {
                    vm.Server.Functions.RemoveAt(i);
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.NewFunction))
            {
                vm.Server.Functions.Add(vm.NewFunction);
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
                vm.Server.Functions = new List<string>();
            }

            if (!string.IsNullOrWhiteSpace(vm.NewFunction))
            {
                vm.Server.Functions.Add(vm.NewFunction);
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
