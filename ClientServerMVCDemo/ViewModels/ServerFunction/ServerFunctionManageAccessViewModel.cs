namespace ClientServerMVCDemo.ViewModels.ServerFunction
{
    public class ServerFunctionManageAccessViewModel
    {
        public Data.Models.ServerFunction Function { get; set; }

        public string SearchTerm { get; set; }

        private List<Data.Models.Client> searchResults { get; set; }
        public List<Data.Models.Client> SearchResults
        {
            get
            {
                return searchResults;
            }
            set 
            {
                searchResults = value; 
                if(searchResults != null)
                {
                    ClientAccess = new bool[searchResults.Count];
                }
            }
        }
        public bool[] ClientAccess { get; set; }
    }
}
