using DoubleV.UsuarioRolesTareas.Application.Models;


namespace DoubleV.UsuarioRolesTareas.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> CrearUsuario(UsuarioModel usuario);
        Task<UsuarioModel> ActualizarUsuario(UsuarioModel usuario);
        Task<UsuarioModel?> ObtenerUsuarioPorId(int id);
        Task<List<UsuarioModel>> ObtenerTodosLosUsuarios();
        Task EliminarUsuarioPorId(int id);
        Task<UsuarioAutenticadoModel?> ObtenerUsuarioPorEmailContraseña(UsuarioModel usuario);

    }
}
