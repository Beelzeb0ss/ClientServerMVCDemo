using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientServerMVCDemo.Data.Models
{
    public class Server
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [NotMapped]
        public IList<string> Functions { get; set; }
    }
}
