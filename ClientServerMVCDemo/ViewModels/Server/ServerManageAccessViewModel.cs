namespace ClientServerMVCDemo.ViewModels.Server
{
    public class ServerManageAccessViewModel
    {
        private Data.Models.Server server;
        public Data.Models.Server Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
                if (server.Functions == null) return;

                bool[] clientsWithAccessToFunction;
                foreach (var f in server.Functions)
                {
                    
                    if(f.Permissions != null)
                    {
                        clientsWithAccessToFunction = new bool[f.Permissions.Count];
                        for(int i = 0; i < clientsWithAccessToFunction.Length; i++)
                        {
                            clientsWithAccessToFunction[i] = true;
                        }

                        ClientsWithAccess.Add(clientsWithAccessToFunction);
                    }
                }
            }
        }

        public List<bool[]> ClientsWithAccess { get; set; } = new List<bool[]>();
    }
}
