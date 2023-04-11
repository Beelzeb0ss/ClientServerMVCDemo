namespace ClientServerMVCDemo.ViewModels.Server
{
    public class ServerEditViewModel
    {
        private Data.Models.Server server;
        public Data.Models.Server Server
        {
            get { return server; }
            set 
            { 
                server = value; 
                if(server.Functions == null)
                {
                    return;
                }

                FunctionsToDelete = new bool[server.Functions.Count()];
            }
        }

        public string NewFunction { get; set; }
        public bool[] FunctionsToDelete { get; set; }
    }
}
