using ClientServerMVCDemo.Services.ClientServices;
using ClientServerMVCDemo.Services.ServerServices;
using Microsoft.AspNetCore.Mvc;

namespace ClientServerMVCDemo.Controllers
{
    public class PermissionCheckController : Controller
    {
        private readonly IClientService clientService;
        private readonly IServerService serverService;

        public PermissionCheckController(IClientService clientService, IServerService serverService)
        {
            this.clientService = clientService;
            this.serverService = serverService;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
