using ClientServerMVCDemo.Services.ClientServices;
using ClientServerMVCDemo.Services.ServerFunction;
using ClientServerMVCDemo.Services.ServerServices;
using ClientServerMVCDemo.ViewModels.PermissionCheck;
using Microsoft.AspNetCore.Mvc;

namespace ClientServerMVCDemo.Controllers
{
    public class PermissionCheckController : Controller
    {
        private readonly IClientService clientService;
        private readonly IServerService serverService;
        private readonly IServerFunctionService serverFunctionService;

        public PermissionCheckController(IClientService clientService, IServerService serverService, IServerFunctionService serverFunctionService)
        {
            this.clientService = clientService;
            this.serverService = serverService;
            this.serverFunctionService = serverFunctionService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new PermissionCheckViewModel();

            vm.Clients = (await clientService.Get(x => true)).ToList();
            vm.Servers = (await serverService.Get(x => true, "Functions")).ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PermissionCheckViewModel vm)
        {
            vm.HasPermission = await serverFunctionService.DoesClientHaveAccess(vm.SelectedClientId, vm.SelectedServerFunctionId);
            vm.WasChecked= true;

            vm.Clients = (await clientService.Get(x => true)).ToList();
            vm.Servers = (await serverService.Get(x => true, "Functions")).ToList();

            return View(vm);
        }
    }
}
