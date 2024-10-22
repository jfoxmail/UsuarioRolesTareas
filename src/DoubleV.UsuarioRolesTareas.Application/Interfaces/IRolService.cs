using DoubleV.UsuarioRolesTareas.Application.Models;

namespace DoubleV.UsuarioRolesTareas.Application.Interfaces
{
    public interface IRolService
    {
        Task<RolModel> CrearRol(RolModel rol);
        Task<RolModel> ActualizarRol(RolModel rol);
        Task<RolModel?> ObtenerRolPorId(int id);
        Task<List<RolModel>> ObtenerTodosLosRoles();
        Task EliminarRolPorId(int id);
    }
}

