using Microsoft.EntityFrameworkCore;
using ServicioDeCupones.Models;
using System.Data;

namespace ServicioDeCupones.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ArticulosModel> Articulos { get; set; }
        public DbSet<CategoriasModel> Categorias { get; set; }
        public DbSet<Cupones_CategoriasModel> Cupones_Categorias { get; set; }
        public DbSet<Cupones_ClientesModel> Cupones_Clientes { get; set; }
        public DbSet<Cupones_DetalleModel> Cupones_Detalles { get; set; }
        public DbSet<Cupones_HistorialModel> Cupones_Historials { get; set; }
        public DbSet<CuponesModel> Cupones { get; set; }
        public DbSet<PreciosModel> Precios { get; set; }
        public DbSet<Tipo_CuponModel> Tipo_Cupon { get; set; }

        // configuracion de relaciones entre PK
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticulosModel>().HasKey(articulo => articulo.id_Articulos );
            modelBuilder.Entity<CategoriasModel>().HasKey(categoria => categoria.id_Categorias);
            modelBuilder.Entity<Cupones_CategoriasModel>().HasKey(cuponCategoria => cuponCategoria.Id_Cupones_Categorias);
            modelBuilder.Entity<Cupones_ClientesModel>().HasKey(cuponCliente => cuponCliente.NroCupon);
            modelBuilder.Entity<Cupones_DetalleModel>().HasKey(cuponDetalle => cuponDetalle.id_Cupon);
            modelBuilder.Entity<Cupones_HistorialModel>().HasKey(cuponHistorial => cuponHistorial.CodCliente);
            modelBuilder.Entity<CuponesModel>().HasKey(cuponModel => cuponModel.id_Cupon);
            modelBuilder.Entity<PreciosModel>().HasKey(precio => precio.Id_Precio);
            modelBuilder.Entity<Tipo_CuponModel>().HasKey(tipoCupon => tipoCupon.id_Tipo_Cupon);
            base.OnModelCreating(modelBuilder);
        }
    }
}
