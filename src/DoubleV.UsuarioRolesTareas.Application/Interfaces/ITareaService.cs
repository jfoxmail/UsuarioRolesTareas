using DoubleV.UsuarioRolesTareas.Application.Models;

namespace DoubleV.UsuarioRolesTareas.Application.Interfaces
{
    public interface ITareaService
    {
        Task<TareaModel> CrearTarea(TareaModel tarea);
        Task<TareaModel> ActualizarTarea(TareaModel tarea);
        Task<TareaModel?> ObtenerTareaPorId(int id);
        Task<List<TareaModel>> ObtenerTodasLasTareas();
        Task<bool> EliminarTareaPorId(int id);
    }
}

