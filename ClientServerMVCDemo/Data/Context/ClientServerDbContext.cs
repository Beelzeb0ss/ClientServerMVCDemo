using ClientServerMVCDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ClientServerMVCDemo.Data.Context
{
    public class ClientServerDbContext : DbContext
    {   
        public DbSet<Client> Clients { get; set; }
        public DbSet<Client> Servers { get; set; }

        public ClientServerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "Client1", Description = "Some client", Properties = new Dictionary<string, string>{ { "SomeProp1" , "Value1" } } },
                new Client { Id = 2, Name = "Client2", Description = "Some other client", Properties = new Dictionary<string, string> { { "SomeProp2", "Value2" } } }
            );

            modelBuilder.Entity<Server>().HasData(
                new Server { Id = 1, Name = "Server1", Description = "Some server", Functions = new List<string>{ "Func1", "Func2"} } ,
                new Server { Id = 2, Name = "Server2", Description = "Some other server", Functions = new List<string> { "Func3", "Func4" } }
            );
        }

    }
}
