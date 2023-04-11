namespace ClientServerMVCDemo.ViewModels.Server
{
    public class ServerListViewModel
    {
        public IEnumerable<Data.Models.Server> Servers { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}
