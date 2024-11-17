using ClientesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ClientesModel> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientesModel>().HasKey(cliente => cliente.CodCliente);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
