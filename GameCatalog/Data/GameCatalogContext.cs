using System;
using System.Collections.Generic;
using GameCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Data;

public partial class GameCatalogContext : DbContext
{

    public GameCatalogContext(DbContextOptions<GameCatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClasificacionJuego> ClasificacionJuegos { get; set; }

    public virtual DbSet<Disponibilidad> Disponibilidads { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<GeneroJuego> GeneroJuegos { get; set; }

    public virtual DbSet<Juego> Juegos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConexionSql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClasificacionJuego>(entity =>
        {
            entity.HasKey(e => e.ClasificacionId).HasName("PK__Clasific__22D135316290B8F5");

            entity.ToTable("ClasificacionJuego");

            entity.Property(e => e.ClasificacionId).HasColumnName("ClasificacionID");
            entity.Property(e => e.DescripcionClasificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Descripcion_Clasificacion");
            entity.Property(e => e.NombreClasificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Clasificacion");
        });

        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.DisponibilidadId).HasName("PK__Disponib__0300F3D4981C6D3C");

            entity.ToTable("Disponibilidad");

            entity.Property(e => e.DisponibilidadId).HasColumnName("DisponibilidadID");
            entity.Property(e => e.NombreDisponibilidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Disponibilidad");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.EmpresaId).HasName("PK__Empresa__7B9F2136A106FD19");

            entity.ToTable("Empresa");

            entity.Property(e => e.EmpresaId).HasColumnName("EmpresaID");
            entity.Property(e => e.InfoEmpresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Info_Empresa");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Empresa");
        });

        modelBuilder.Entity<GeneroJuego>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PK__GeneroJu__A99D02686F668793");

            entity.ToTable("GeneroJuego");

            entity.Property(e => e.GeneroId).HasColumnName("GeneroID");
            entity.Property(e => e.DescripcionGenero)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Descripcion_Genero");
            entity.Property(e => e.NombreGenero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Genero");
        });

        modelBuilder.Entity<Juego>(entity =>
        {
            entity.HasKey(e => e.JuegoId).HasName("PK__Juego__F76C1B2507C87E50");

            entity.ToTable("Juego");

            entity.Property(e => e.JuegoId).HasColumnName("JuegoID");
            entity.Property(e => e.ClasificacionJuegoId).HasColumnName("Clasificacion_JuegoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DisponibilidadJuegoId).HasColumnName("Disponibilidad_JuegoID");
            entity.Property(e => e.EmpresaJuegoId).HasColumnName("Empresa_JuegoID");
            entity.Property(e => e.FechaEstreno)
                .HasColumnType("date")
                .HasColumnName("Fecha_estreno");
            entity.Property(e => e.GeneroJuegoId).HasColumnName("Genero_JuegoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ClasificacionJuego).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.ClasificacionJuegoId)
                .HasConstraintName("FK__Juego__Clasifica__2D27B809");

            entity.HasOne(d => d.DisponibilidadJuego).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.DisponibilidadJuegoId)
                .HasConstraintName("FK__Juego__Disponibi__2F10007B");

            entity.HasOne(d => d.EmpresaJuego).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.EmpresaJuegoId)
                .HasConstraintName("FK__Juego__Empresa_J__2E1BDC42");

            entity.HasOne(d => d.GeneroJuego).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.GeneroJuegoId)
                .HasConstraintName("FK__Juego__Genero_Ju__2C3393D0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
