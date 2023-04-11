using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerMVCDemo.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("ClientProperties")]
        public IDictionary<string, string> Properties { get;set; }
    }
}
