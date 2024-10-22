using DoubleV.UsuarioRolesTareas.Application.Models;

namespace DoubleV.UsuarioRolesTareas.Application.Interfaces
{
    public interface IEstadoService
    {
        Task<EstadoModel> CrearEstado(EstadoModel estado);
        Task<EstadoModel> ActualizarEstado(EstadoModel estado);
        Task<EstadoModel?> ObtenerEstadoPorId(int id);
        Task<List<EstadoModel>> ObtenerTodosLosEstados();
        Task EliminarEstadoPorId(int id);
    }
}

