using GestorPrestamos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPrestamos.Infrastructure.Persistence;

public sealed class GestorPrestamosDbContext : DbContext
{
    public GestorPrestamosDbContext(DbContextOptions<GestorPrestamosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CodigoInventario).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Marca).HasMaxLength(80).IsRequired();
            entity.Property(e => e.Ubicacion).HasMaxLength(80).IsRequired();
            entity.Property(e => e.ObservacionEstado).HasMaxLength(250);

            entity.HasDiscriminator<string>("TipoEquipo")
                .HasValue<Camara>("Camara")
                .HasValue<Proyector>("Proyector")
                .HasValue<Accesorio>("Accesorio");
        });

        modelBuilder.Entity<Camara>()
            .Property(c => c.Resolucion)
            .HasMaxLength(50);

        modelBuilder.Entity<Camara>()
            .Property(c => c.TipoLente)
            .HasMaxLength(50);

        modelBuilder.Entity<Proyector>()
            .Property(p => p.ResolucionNativa)
            .HasMaxLength(50);

        modelBuilder.Entity<Accesorio>()
            .Property(a => a.Categoria)
            .HasMaxLength(50);

        modelBuilder.Entity<Accesorio>()
            .Property(a => a.Compatibilidad)
            .HasMaxLength(100);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.NombreCompleto).HasMaxLength(120).IsRequired();
            entity.Property(u => u.Matricula).HasMaxLength(20).IsRequired();
            entity.Property(u => u.Correo).HasMaxLength(100);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.ObservacionesEntrega).HasMaxLength(250);
            entity.Property(p => p.ObservacionesDevolucion).HasMaxLength(250);
            entity.Property(p => p.CostoDanio).HasColumnType("decimal(10,2)");

            entity.HasOne(p => p.Usuario)
                .WithMany(u => u.Prestamos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Equipo)
                .WithMany()
                .HasForeignKey(p => p.EquipoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
