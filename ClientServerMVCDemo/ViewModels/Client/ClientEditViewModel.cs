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

                PropertiesToDelete = new Dictionary<string, bool>();
                foreach (var kvp in client.Properties) 
                {
                    PropertiesToDelete.Add(kvp.Key, false);
                }
            }
        }

        public string NewPropertyName { get; set; }
        public string NewPropertyValue { get; set; }
        public Dictionary<string, bool> PropertiesToDelete { get;set; }
    }
}
