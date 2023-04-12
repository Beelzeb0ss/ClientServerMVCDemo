namespace ClientServerMVCDemo.ViewModels.Client
{
    public class ClientEditViewModel
    {
        private Data.Models.Client client;
        public Data.Models.Client Client 
        {
            get { return client; }
            set 
            {
                client = value;
                if (client.Properties == null)
                {
                    return;
                }

                PropertiesToDelete = new List<bool>();
                for (int i = 0; i < Client.Properties.Count; i++)
                {
                    PropertiesToDelete.Add(false);
                }
            }
        }

        public string NewPropertyName { get; set; }
        public string NewPropertyValue { get; set; }
        public List<bool> PropertiesToDelete { get;set; } = new List<bool>();
    }
}
