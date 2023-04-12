using ClientServerMVCDemo.Data.Models;
using ClientServerMVCDemo.Services.ClientServices;
using ClientServerMVCDemo.Services.ServerFunction;
using ClientServerMVCDemo.Services.ServerServices;
using ClientServerMVCDemo.ViewModels.Server;
using ClientServerMVCDemo.ViewModels.ServerFunction;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientServerMVCDemo.Controllers
{
    public class ServerController : Controller
    {
        private readonly IServerService serverService;
        private readonly IServerFunctionService serverFunctionService;
        private readonly IClientService clientService;

        public ServerController(IServerService serverService, IServerFunctionService serverFunctionService, IClientService clientService)
        {
            this.serverService = serverService;
            this.serverFunctionService = serverFunctionService;
            this.clientService = clientService;
        }

        public async Task<IActionResult> Index(int page)
        {
            var viewModel = new ServerListViewModel();

            if (page == 0)
            {
                page = 1;
            }
            var pageData = await serverService.GetPage(page, 3, "Functions,Functions.Permissions,Functions.Permissions.Client");

            viewModel.Servers = pageData.ToList();
            viewModel.CurrentPage = pageData.PageIndex;
            viewModel.HasNextPage = pageData.HasNextPage;
            viewModel.HasPreviousPage = pageData.HasPreviousPage;

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = new ServerEditViewModel();

            viewModel.Server = await serverService.GetById(id, "Functions");

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

        public async Task<IActionResult> ManageAccess(int id)
        {
            var vm = new ServerManageAccessViewModel();

            var server = await serverService.GetById(id, "Functions,Functions.Permissions,Functions.Permissions.Client");

            vm.Server = server;
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAccess(ServerManageAccessViewModel vm)
        {
            for(int i = 0; i < vm.ClientsWithAccess.Count; i++)
            {
                for(int j = vm.ClientsWithAccess[i].Length - 1; j >= 0; j--)
                {
                    if (!vm.ClientsWithAccess[i][j])
                    {
                        await serverService.DeleteFunctionPermission(vm.Server.Functions[i].Permissions[j].Id);
                    }
                }
            }

            return RedirectToAction("ManageAccess", new { id = vm.Server.Id });
        }

        public async Task<IActionResult> ManageFunctionAccess(int id)
        {
            var function = await serverFunctionService.GetById(id, "Permissions,Permissions.Client");

            var vm = new ServerFunctionManageAccessViewModel();
            vm.Function = function;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ManageFunctionAccess(ServerFunctionManageAccessViewModel vm)
        {
            if (vm.ClientAccess != null)
            {
                for (int i = 0; i < vm.ClientAccess.Length; i++)
                {
                    if (vm.ClientAccess[i])
                    {
                        await serverFunctionService.AddPermissionForClient(vm.SearchResults[i], vm.Function);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.SearchTerm))
            {
                var results = await clientService.SearchByName(vm.SearchTerm , "FunctionPermissions");
                for (int i = results.Count - 1; i >= 0; i--)
                {
                    if(await serverFunctionService.DoesClientHaveAccess(results[i], vm.Function))
                    {
                        results.RemoveAt(i);
                    }
                }
                vm.SearchResults = results;
            }

            vm.Function = await serverFunctionService.GetById(vm.Function.Id, "Permissions,Permissions.Client");

            return View(vm);
        }
    }
}
