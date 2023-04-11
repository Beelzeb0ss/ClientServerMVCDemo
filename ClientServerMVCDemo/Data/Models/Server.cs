using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerMVCDemo.Data.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public ICollection<string> Functions { get; set; }
    }
}
