using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication9.Models;

public partial class SistemaDeConcursosContext : DbContext
{
    public SistemaDeConcursosContext()
    {
    }

    public SistemaDeConcursosContext(DbContextOptions<SistemaDeConcursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoría> Categorías { get; set; }

    public virtual DbSet<Concurso> Concursos { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Jurado> Jurados { get; set; }

    public virtual DbSet<Premio> Premios { get; set; }

    public virtual DbSet<PuntuacionesJurado> PuntuacionesJurados { get; set; }

    public virtual DbSet<Resultado> Resultados { get; set; }

    public virtual DbSet<TiposDeConcurso> TiposDeConcursos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D9IF75B\\SQL2019;Database=Sistema_de_Concursos;User Id=DESKTOP-D9IF75B\\SQL2019;Password='';Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoría>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categorí__CD54BC5ACD2FC303");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<Concurso>(entity =>
        {
            entity.HasKey(e => e.IdConcurso).HasName("PK__Concurso__9809756799057081");

            entity.Property(e => e.IdConcurso).HasColumnName("id_concurso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdTipoConcurso).HasColumnName("id_tipo_concurso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Concursos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Concursos__id_ca__1A14E395");

            entity.HasOne(d => d.IdTipoConcursoNavigation).WithMany(p => p.Concursos)
                .HasForeignKey(d => d.IdTipoConcurso)
                .HasConstraintName("FK__Concursos__id_ti__1B0907CE");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__Inscripc__CB0117BA687769C9");

            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_inscripcion");
            entity.Property(e => e.IdConcurso).HasColumnName("id_concurso");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdConcursoNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdConcurso)
                .HasConstraintName("FK__Inscripci__id_co__1ED998B2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Inscripci__id_us__1DE57479");
        });

        modelBuilder.Entity<Jurado>(entity =>
        {
            entity.HasKey(e => e.IdJurado).HasName("PK__Jurado__F1E51465ABE9C4C2");

            entity.ToTable("Jurado");

            entity.Property(e => e.IdJurado).HasColumnName("id_jurado");
            entity.Property(e => e.IdConcurso).HasColumnName("id_concurso");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdConcursoNavigation).WithMany(p => p.Jurados)
                .HasForeignKey(d => d.IdConcurso)
                .HasConstraintName("FK__Jurado__id_concu__239E4DCF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Jurados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Jurado__id_usuar__22AA2996");
        });

        modelBuilder.Entity<Premio>(entity =>
        {
            entity.HasKey(e => e.IdPremio).HasName("PK__Premios__A98947455DB49CC6");

            entity.Property(e => e.IdPremio).HasColumnName("id_premio");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdConcurso).HasColumnName("id_concurso");

            entity.HasOne(d => d.IdConcursoNavigation).WithMany(p => p.Premios)
                .HasForeignKey(d => d.IdConcurso)
                .HasConstraintName("FK__Premios__id_conc__2B3F6F97");
        });

        modelBuilder.Entity<PuntuacionesJurado>(entity =>
        {
            entity.HasKey(e => e.IdPuntuacion).HasName("PK__Puntuaci__1B2258DF62D9E72F");

            entity.ToTable("Puntuaciones_Jurados");

            entity.Property(e => e.IdPuntuacion).HasColumnName("id_puntuacion");
            entity.Property(e => e.Comentarios)
                .HasMaxLength(255)
                .HasColumnName("comentarios");
            entity.Property(e => e.IdConcurso).HasColumnName("id_concurso");
            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.IdJurado).HasColumnName("id_jurado");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");
            entity.Property(e => e.Razon)
                .HasMaxLength(255)
                .HasColumnName("razon");

            entity.HasOne(d => d.IdConcursoNavigation).WithMany(p => p.PuntuacionesJurados)
                .HasForeignKey(d => d.IdConcurso)
                .HasConstraintName("FK__Puntuacio__id_co__276EDEB3");

            entity.HasOne(d => d.IdInscripcionNavigation).WithMany(p => p.PuntuacionesJurados)
                .HasForeignKey(d => d.IdInscripcion)
                .HasConstraintName("FK__Puntuacio__id_in__286302EC");

            entity.HasOne(d => d.IdJuradoNavigation).WithMany(p => p.PuntuacionesJurados)
                .HasForeignKey(d => d.IdJurado)
                .HasConstraintName("FK__Puntuacio__id_ju__267ABA7A");
        });

        modelBuilder.Entity<Resultado>(entity =>
        {
            entity.HasKey(e => e.IdResultado).HasName("PK__Resultad__33A42B30B3AEE0C9");

            entity.Property(e => e.IdResultado).HasColumnName("id_resultado");
            entity.Property(e => e.Clasificacion).HasColumnName("clasificacion");
            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.PuntuacionFinal).HasColumnName("puntuacion_final");

            entity.HasOne(d => d.IdInscripcionNavigation).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.IdInscripcion)
                .HasConstraintName("FK__Resultado__id_in__2E1BDC42");
        });

        modelBuilder.Entity<TiposDeConcurso>(entity =>
        {
            entity.HasKey(e => e.IdTipoConcurso).HasName("PK__Tipos_de__483ABA662783AF1A");

            entity.ToTable("Tipos_de_Concurso");

            entity.Property(e => e.IdTipoConcurso).HasColumnName("id_tipo_concurso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(100)
                .HasColumnName("nombre_tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04ADD698D17F");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__AB6E6164FEAC44CB").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .HasColumnName("tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
