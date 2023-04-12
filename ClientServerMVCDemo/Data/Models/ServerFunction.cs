using System.ComponentModel.DataAnnotations;

namespace ClientServerMVCDemo.Data.Models
{
    public class ServerFunction
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }

        public IList<ClientFunctionPermission> Permissions { get; set; }
    }
}
