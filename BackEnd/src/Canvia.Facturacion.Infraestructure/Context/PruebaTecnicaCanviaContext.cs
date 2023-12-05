using Canvia.Facturacion.Domain.EntitiesEntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Canvia.Facturacion.Infraestructure.Context;

public partial class PruebaTecnicaCanviaContext : DbContext
{
    public PruebaTecnicaCanviaContext()
    {
    }

    public PruebaTecnicaCanviaContext(DbContextOptions<PruebaTecnicaCanviaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; } = null!;
    public virtual DbSet<FacturaCabecera> FacturaCabeceras { get; set; } = null!;
    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; } = null!;

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("CLIENTE");

            entity.Property(e => e.ClienteId)
                .ValueGeneratedNever()
                .HasColumnName("ClienteID");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FacturaCabecera>(entity =>
        {
            entity.HasKey(e => e.FacturaId)
                .HasName("PK__FACTURA___5C024805D70035E6");

            entity.ToTable("FACTURA_CABECERA");

            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(299)
                .IsUnicode(false);

            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

            entity.Property(e => e.FechaEmision).HasColumnType("date");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente)
                .WithMany(p => p.FacturaCabeceras)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__FACTURA_C__Clien__398D8EEE");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => new { e.DetalleId, e.FacturaId })
                .HasName("PK__FACTURA___2BD9F27AD5D2C104");

            entity.ToTable("FACTURA_DETALLE");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");

            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Factura)
                .WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FACTURA_D__Factu__5FB337D6");
        });


        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
