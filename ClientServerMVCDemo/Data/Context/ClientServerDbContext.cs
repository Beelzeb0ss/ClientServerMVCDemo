using ClientServerMVCDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Xml;

namespace ClientServerMVCDemo.Data.Context
{
    public class ClientServerDbContext : DbContext
    {   
        public DbSet<Client> Clients { get; set; }
        public DbSet<Client> Servers { get; set; }
        public DbSet<ClientProperty> Properties { get; set; }
        public DbSet<ServerFunction> Functions { get; set; }

        public ClientServerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            //doesn't work in memory - cascade delete
            modelBuilder.Entity<Client>().HasMany(x => x.Properties).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Server>().HasMany(x => x.Functions).WithOne(x => x.Server).HasForeignKey(x => x.ServerId).OnDelete(DeleteBehavior.Cascade);

            var client = new Client { Id = 1, Name = "Client1", Description = "Some client" };
            var client2 = new Client { Id = 2, Name = "Клиент2", Description = "Някъв клиент" };

            modelBuilder.Entity<Client>().HasData(client, client2);

            var clientProp1 = new ClientProperty { Id = 1, Key = "Prop1", Value = "Value1", ClientId = client.Id };
            var clientProp2 = new ClientProperty { Id = 2, Key = "Prop2", Value = "Value2", ClientId = client.Id };
            var clientProp3 = new ClientProperty { Id = 3, Key = "Свойство?", Value = "Стойност1", ClientId = client2.Id };

            modelBuilder.Entity<ClientProperty>().HasData(clientProp1, clientProp2, clientProp3);

            var server = new Server { Id = 1, Name = "Server1", Description = "Some server" };
            var server2 = new Server { Id = 2, Name = "Сървър2", Description = "Някъв сървър" };

            modelBuilder.Entity<Server>().HasData(server, server2);

            var serverFunc1 = new ServerFunction { Id = 1, Name = "Func1", ServerId = server.Id };
            var serverFunc2 = new ServerFunction { Id = 2, Name = "Func2", ServerId = server.Id };
            var serverFunc3 = new ServerFunction { Id = 3, Name = "Функция3", ServerId = server2.Id };

            modelBuilder.Entity<ServerFunction>().HasData(serverFunc1, serverFunc2, serverFunc3);
        }

    }
}
