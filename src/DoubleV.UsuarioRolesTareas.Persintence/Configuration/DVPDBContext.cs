using System;
using System.Collections.Generic;
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.UsuarioRolesTareas.Persintence;

public partial class DVPDBContext : DbContext, IDVPDBContext
{
    public DVPDBContext()
    {
    }

    public DVPDBContext(DbContextOptions<DVPDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Pagina> Paginas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:dvp-sqlserver-01.database.windows.net,1433;Initial Catalog=dvp-database-01;Persist Security Info=False;User ID=dvp-admin-01;Password=QazResCon123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagina>(entity =>
        {
            entity.HasOne(d => d.Rol).WithMany(p => p.Paginas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagina_Rol");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasOne(d => d.Estado).WithMany(p => p.Tareas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarea_Estado");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Tareas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarea_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
