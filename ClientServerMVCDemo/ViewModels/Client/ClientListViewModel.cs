namespace ClientServerMVCDemo.ViewModels.Client
{
    public class ClientListViewModel
    {
        public IEnumerable<Data.Models.Client> Clients { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set;}
    }
}
