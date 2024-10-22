using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DoubleV.UsuarioRolesTareas.Application.DataBase
{
    public interface IDVPDBContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Rol> Rols { get; set; }
        DbSet<Tarea> Tareas { get; set; }
        DbSet<Estado> Estados { get; set; }
        DbSet<Pagina> Paginas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
