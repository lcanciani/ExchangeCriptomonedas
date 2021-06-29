using System;
using System.IO;
using BackEndExchange.Model.PropositoGeneral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

#nullable disable

namespace BackEndExchange.Model
{
    public partial class ExchangeDBContext : DbContext
    {
        private static IConfiguration config { get; set; }
        //private readonly AppSettings _connectionString;

        public ExchangeDBContext()
        {

        }

        public ExchangeDBContext(DbContextOptions<ExchangeDBContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Banco> Bancos { get; set; }
        public virtual DbSet<Billetera> Billeteras { get; set; }
        public virtual DbSet<Criptomoneda> Criptomonedas { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<MovimientosCripto> MovimientosCriptos { get; set; }
        public virtual DbSet<MovimientosFiat> MovimientosFiats { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
               // optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ExchangeDB;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(GetConnectionString());
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

                entity.Property(e => e.DireccionBilletera)
                    .HasMaxLength(50)
                    .HasColumnName("direccionBilletera")
                    .IsFixedLength(true);

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.IdMovimientoCripto).HasColumnName("idMovimientoCripto");

                entity.Property(e => e.IdMovimientoFiat).HasColumnName("idMovimientoFiat");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("saldo");

                entity.HasOne(d => d.IdCriptomonedaNavigation)
                    .WithMany(p => p.Billeteras)
                    .HasForeignKey(d => d.IdCriptomoneda)
                    .HasConstraintName("FK_Billetera_Criptomonedas");

                entity.HasOne(d => d.IdMovimientoCriptoNavigation)
                    .WithMany(p => p.Billeteras)
                    .HasForeignKey(d => d.IdMovimientoCripto)
                    .HasConstraintName("FK_Billetera_MovimientosCripto");

                entity.HasOne(d => d.IdMovimientoFiatNavigation)
                    .WithMany(p => p.Billeteras)
                    .HasForeignKey(d => d.IdMovimientoFiat)
                    .HasConstraintName("FK_Billetera_MovimientosFiat");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Billeteras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Billetera_Usuarios");
            });

            modelBuilder.Entity<Criptomoneda>(entity =>
            {
                entity.HasKey(e => e.IdCriptomoneda);

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.Capitalizacion)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("capitalizacion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("precio");

                entity.Property(e => e.Simbolo)
                    .HasMaxLength(10)
                    .HasColumnName("simbolo")
                    .IsFixedLength(true);

                entity.Property(e => e.Stock)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("stock");

                entity.Property(e => e.ValorTotal)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("valorTotal");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura);

                entity.ToTable("DetalleFactura");

                entity.Property(e => e.IdDetalleFactura).HasColumnName("idDetalleFactura");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.IdFactura).HasColumnName("idFactura");

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

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Facturas_Usuarios");
            });

            modelBuilder.Entity<MovimientosCripto>(entity =>
            {
                entity.HasKey(e => e.IdMovimientoCripto);

                entity.ToTable("MovimientosCripto");

                entity.Property(e => e.IdMovimientoCripto).HasColumnName("idMovimientoCripto");

                entity.Property(e => e.IdBanco).HasColumnName("idBanco");

                entity.Property(e => e.IdCriptomoneda).HasColumnName("idCriptomoneda");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("monto");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(30)
                    .HasColumnName("tipo")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdBancoNavigation)
                    .WithMany(p => p.MovimientosCriptos)
                    .HasForeignKey(d => d.IdBanco)
                    .HasConstraintName("FK_MovimientosCripto_Bancos");

                entity.HasOne(d => d.IdCriptomonedaNavigation)
                    .WithMany(p => p.MovimientosCriptos)
                    .HasForeignKey(d => d.IdCriptomoneda)
                    .HasConstraintName("FK_MovimientosCripto_Criptomonedas");
            });

            modelBuilder.Entity<MovimientosFiat>(entity =>
            {
                entity.HasKey(e => e.IdMovimientoFiat);

                entity.ToTable("MovimientosFiat");

                entity.Property(e => e.IdMovimientoFiat).HasColumnName("idMovimientoFiat");

                entity.Property(e => e.Cbu).HasColumnName("cbu");

                entity.Property(e => e.IdBanco).HasColumnName("idBanco");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("monto");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(10)
                    .HasColumnName("tipo")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdBancoNavigation)
                    .WithMany(p => p.MovimientosFiats)
                    .HasForeignKey(d => d.IdBanco)
                    .HasConstraintName("FK_MovimientosFiat_Bancos");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .HasColumnName("apellido")
                    .IsFixedLength(true);

                entity.Property(e => e.ClavePrivada)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("clavePrivada");

                entity.Property(e => e.ClavePublica)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("clavePublica");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .HasColumnName("contrasenia")
                    .IsFixedLength(true);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.SaldoFiat)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("saldoFiat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            config = builder.Build();

            return config.GetConnectionString("DevConecction");

        }
    }
}
