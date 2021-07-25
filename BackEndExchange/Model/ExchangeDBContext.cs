using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class ExchangeDBContext : DbContext
    {
        public ExchangeDBContext()
        {
        }

        public ExchangeDBContext(DbContextOptions<ExchangeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banco> Bancos { get; set; }
        public virtual DbSet<Billetera> Billeteras { get; set; }
        public virtual DbSet<Cotizacion> Cotizacions { get; set; }
        public virtual DbSet<Criptomoneda> Criptomonedas { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<TiposMovimiento> TiposMovimientos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.1.50;Initial Catalog=ExchangeDB;User ID=sa;Password=sqlLove;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.IdBanco);

                entity.Property(e => e.IdBanco).HasColumnName("idBanco");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(30)
                    .HasColumnName("ciudad")
                    .IsFixedLength(true);

                entity.Property(e => e.Ciut)
                    .HasMaxLength(30)
                    .HasColumnName("ciut")
                    .IsFixedLength(true);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(30)
                    .HasColumnName("direccion")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("date")
                    .HasColumnName("fechaBaja");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(30)
                    .HasColumnName("razonSocial")
                    .IsFixedLength(true);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .HasColumnName("telefono")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Billetera>(entity =>
            {
                entity.HasKey(e => e.IdBilletera);

                entity.ToTable("Billetera");

                entity.Property(e => e.IdBilletera).HasColumnName("idBilletera");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.ClavePrivada)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("clavePrivada");

                entity.Property(e => e.ClavePublica)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("clavePublica");

                entity.Property(e => e.DireccionBilletera)
                    .HasMaxLength(50)
                    .HasColumnName("direccionBilletera")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("date")
                    .HasColumnName("fechaBaja");

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdCriptomonedaNavigation)
                    .WithMany(p => p.Billeteras)
                    .HasForeignKey(d => d.IdCriptomoneda)
                    .HasConstraintName("FK_Billetera_Criptomonedas1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Billeteras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Billetera_Usuarios");
            });

            modelBuilder.Entity<Cotizacion>(entity =>
            {
                entity.HasKey(e => e.IdCotizacion);

                entity.ToTable("Cotizacion");

                entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");

                entity.Property(e => e.CotizacionPesos)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("cotizacionPesos");

                entity.Property(e => e.Divisa)
                    .HasMaxLength(50)
                    .HasColumnName("divisa")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Criptomoneda>(entity =>
            {
                entity.HasKey(e => e.IdCriptomoneda);

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("date")
                    .HasColumnName("fechaBaja");

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(100)
                    .HasColumnName("imagenUrl")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.PorcentajeGanancia)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("porcentajeGanancia");

                entity.Property(e => e.PrecioCompra)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("precioCompra");

                entity.Property(e => e.Simbolo)
                    .HasMaxLength(10)
                    .HasColumnName("simbolo")
                    .IsFixedLength(true);

                entity.Property(e => e.StockDisponible)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("stockDisponible");

                entity.Property(e => e.StockTotal)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("stockTotal");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura);

                entity.ToTable("DetalleFactura");

                entity.Property(e => e.IdDetalleFactura).HasColumnName("idDetalleFactura");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.Comision)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("comision");

                entity.Property(e => e.CotizacionDolar)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("cotizacionDolar");

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.IdFactura).HasColumnName("idFactura");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("monto");

                entity.Property(e => e.PorcentajeGanancia)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("porcentajeGanancia");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdCriptomonedaNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdCriptomoneda)
                    .HasConstraintName("FK_DetalleFactura_Criptomonedas");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdFactura)
                    .HasConstraintName("FK_DetalleFactura_Facturas");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.Property(e => e.IdFactura).HasColumnName("idFactura");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdBanco).HasColumnName("idBanco");

                entity.Property(e => e.IdTipoMovimiento).HasColumnName("idTipoMovimiento");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdBancoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdBanco)
                    .HasConstraintName("FK_Facturas_Bancos");

                entity.HasOne(d => d.IdTipoMovimientoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdTipoMovimiento)
                    .HasConstraintName("FK_Facturas_TiposMovimiento");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Facturas_Usuarios");
            });

            modelBuilder.Entity<TiposMovimiento>(entity =>
            {
                entity.HasKey(e => e.IdTiposMovimiento)
                    .HasName("PK_TiposMovimiento_1");

                entity.ToTable("TiposMovimiento");

                entity.Property(e => e.IdTiposMovimiento).HasColumnName("idTiposMovimiento");

                entity.Property(e => e.Comision)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("comision");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("tipo")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido")
                    .IsFixedLength(true);

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .HasColumnName("contrasenia")
                    .IsFixedLength(true);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion")
                    .IsFixedLength(true);

                entity.Property(e => e.Dni)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dni")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("date")
                    .HasColumnName("fechaBaja");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.SaldoFiatUsuario)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("saldoFiatUsuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
