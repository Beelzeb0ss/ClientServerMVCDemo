using ClientServerMVCDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            modelBuilder.Entity<Client>()
                .Property(c => c.Properties)
                .HasConversion(
                    d => JsonConvert.SerializeObject(d),
                    s => JsonConvert.DeserializeObject<Dictionary<string, string>>(s))
                .HasColumnName("PropertiesJson").IsRequired(false);

            modelBuilder.Entity<Server>()
                .Property(x => x.Functions)
                .HasConversion(
                    d => JsonConvert.SerializeObject(d),
                    s => JsonConvert.DeserializeObject<IList<string>>(s))
                .HasColumnName("FunctionsJson").IsRequired(false); 

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "Client1", Description = "Some client", Properties = new Dictionary<string, string>{ { "SomeProp1" , "Value1" } } },
                new Client { Id = 2, Name = "Client2", Description = "Some other client", Properties = new Dictionary<string, string> { { "SomeProp2", "Value2" } } },
                new Client { Id = 3, Name = "Клиент1", Description = "Дали работи на български?", Properties = new Dictionary<string, string> { { "Нещо", "стойност" } } },
                new Client { Id = 4, Name = "Клиент2", Description = "описание", Properties = new Dictionary<string, string> {  } }
            );

            modelBuilder.Entity<Server>().HasData(
                new Server { Id = 1, Name = "Server1", Description = "Some server", Functions = new List<string>{ "Func1", "Func2"} } ,
                new Server { Id = 2, Name = "Server2", Description = "Some other server", Functions = new List<string> { "Func3", "Func4" } }
            );
        }

    }
}
