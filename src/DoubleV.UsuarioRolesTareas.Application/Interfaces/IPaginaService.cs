using DoubleV.UsuarioRolesTareas.Application.Models;

namespace DoubleV.UsuarioRolesTareas.Application.Interfaces
{
    public interface IPaginaService
    {
        Task<PaginaModel> CrearPagina(PaginaModel pagina);
        Task<PaginaModel> ActualizarPagina(PaginaModel pagina);
        Task<PaginaModel?> ObtenerPaginaPorId(int id);
        Task<List<PaginaModel>> ObtenerTodasLasPaginas();
        Task EliminarPaginaPorId(int id);
    }
}

