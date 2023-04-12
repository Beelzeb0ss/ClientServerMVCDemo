using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientServerMVCDemo.ViewModels.PermissionCheck
{
    public class PermissionCheckViewModel
    {
        private List<Data.Models.Client> clients;
        public List<Data.Models.Client> Clients 
        {
            get 
            { 
                return clients;
            }
            set
            {
                clients = value;
                ClientDopdownItems = new List<SelectListItem>();
                foreach(var client in clients)
                {
                    ClientDopdownItems.Add(new SelectListItem { Text = client.Name, Value = client.Id.ToString() });
                }
            } 
        }

        private List<Data.Models.Server> servers { get; set; }
        public List<Data.Models.Server> Servers
        {
            get
            {
                return servers;
            }
            set
            {
                servers = value;
                ServerFunctionsDopdownItems = new List<SelectListItem>();
                foreach(var server in servers)
                {
                    foreach(var func in server.Functions)
                    {
                        ServerFunctionsDopdownItems.Add(new SelectListItem { Text = (server.Name + "/" + func.Name), Value = func.Id.ToString()});
                    }
                }
            }
        }

        public List<SelectListItem> ClientDopdownItems { get; set; }
        public List<SelectListItem> ServerFunctionsDopdownItems { get; set; }

        public bool HasPermission { get;set; }
        public bool WasChecked { get; set; }

        public int SelectedClientId { get; set; }
        public int SelectedServerFunctionId { get; set; }
    }
}
