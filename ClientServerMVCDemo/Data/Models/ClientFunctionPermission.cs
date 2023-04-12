namespace ClientServerMVCDemo.Data.Models
{
    public class ClientFunctionPermission
    {
        public int Id { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }
        
        public ServerFunction ServerFunction { get; set; }
        public int ServerFunctionId { get; set; } 
    }
}
