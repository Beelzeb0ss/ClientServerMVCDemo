namespace ClientServerMVCDemo.Data.Models
{
    public class ServerFunction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }
    }
}
