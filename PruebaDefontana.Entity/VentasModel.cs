using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PruebaDefontana.PruebaDefontana.Entity
{
    public partial class VentasModel : DbContext
    {
        public VentasModel()
            : base("name=VentasModel")
        {
        }

        public virtual DbSet<Local> Locales { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Local>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Local>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Local>()
                .HasMany(e => e.Ventas)
                .WithRequired(e => e.Local)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .HasMany(e => e.Productos)
                .WithRequired(e => e.Marca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.VentaDetalles)
                .WithRequired(e => e.Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venta>()
                .HasMany(e => e.VentaDetalles)
                .WithRequired(e => e.Venta)
                .WillCascadeOnDelete(false);
        }
    }
}
