using DoubleV.UsuarioRolesTareas.Application.Exceptions;
using DoubleV.UsuarioRolesTareas.Application.Features;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.UsuarioRolesTareas.Api.Controllers
{
    [Route("api/v1/paginas")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class PaginaController : ControllerBase
    {
        /// <summary>
        /// Crea una nueva página en el sistema.
        /// </summary>
        /// <param name="pagina">El objeto Pagina que contiene los datos de la nueva página.</param>
        /// <param name="paginaService">El servicio de páginas que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Pagina creado y un código de estado 201 Created.</returns>
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearPagina([FromBody] PaginaModel pagina, [FromServices] IPaginaService paginaService)
        {
            var data = await paginaService.CrearPagina(pagina);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        /// <summary>
        /// Actualiza los datos de una página existente.
        /// </summary>
        /// <param name="pagina">El objeto Pagina con los datos actualizados.</param>
        /// <param name="paginaService">El servicio de páginas que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Pagina actualizado y un código de estado 200 OK.</returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarPagina([FromBody] PaginaModel pagina, [FromServices] IPaginaService paginaService)
        {
            var data = await paginaService.ActualizarPagina(pagina);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Elimina una página por su ID.
        /// </summary>
        /// <param name="id">El ID de la página que se va a eliminar.</param>
        /// <param name="paginaService">El servicio de páginas que maneja la lógica de negocio.</param>
        /// <returns>Retorna un código de estado 204 No Content si la eliminación es exitosa.</returns>
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarPagina([FromRoute] int id, [FromServices] IPaginaService paginaService)
        {
            await paginaService.EliminarPaginaPorId(id);
            return StatusCode(StatusCodes.Status204NoContent, ResponseApiService.Response(StatusCodes.Status204NoContent, "Página eliminada"));
        }

        /// <summary>
        /// Obtiene una página por su ID.
        /// </summary>
        /// <param name="id">El ID de la página que se va a obtener.</param>
        /// <param name="paginaService">El servicio de páginas que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Pagina si se encuentra, o un código de estado 404 Not Found si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPaginaPorId([FromRoute] int id, [FromServices] IPaginaService paginaService)
        {
            var pagina = await paginaService.ObtenerPaginaPorId(id);
            if (pagina == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, "Página no encontrada"));
            }
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, pagina));
        }

        /// <summary>
        /// Obtiene todas las páginas registradas.
        /// </summary>
        /// <param name="paginaService">El servicio de páginas que maneja la lógica de negocio.</param>
        /// <returns>Retorna una lista de todas las páginas y un código de estado 200 OK.</returns>
        [HttpGet("Todas")]
        public async Task<IActionResult> ObtenerTodasLasPaginas([FromServices] IPaginaService paginaService)
        {
            var paginas = await paginaService.ObtenerTodasLasPaginas();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, paginas));
        }
    }
}
