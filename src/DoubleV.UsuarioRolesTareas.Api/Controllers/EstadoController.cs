using DoubleV.UsuarioRolesTareas.Application.Exceptions;
using DoubleV.UsuarioRolesTareas.Application.Features;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.UsuarioRolesTareas.Api.Controllers
{
    [Route("api/v1/estados")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class EstadoController : ControllerBase
    {
        /// <summary>
        /// Crea un nuevo estado en el sistema.
        /// </summary>
        /// <param name="estado">El objeto Estado que contiene los datos del nuevo estado.</param>
        /// <param name="estadoService">El servicio de estados que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Estado creado y un código de estado 201 Created.</returns>
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearEstado([FromBody] EstadoModel estado, [FromServices] IEstadoService estadoService)
        {
            var data = await estadoService.CrearEstado(estado);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        /// <summary>
        /// Actualiza los datos de un estado existente.
        /// </summary>
        /// <param name="estado">El objeto Estado con los datos actualizados.</param>
        /// <param name="estadoService">El servicio de estados que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Estado actualizado y un código de estado 200 OK.</returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarEstado([FromBody] EstadoModel estado, [FromServices] IEstadoService estadoService)
        {
            var data = await estadoService.ActualizarEstado(estado);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Elimina un estado por su ID.
        /// </summary>
        /// <param name="id">El ID del estado que se va a eliminar.</param>
        /// <param name="estadoService">El servicio de estados que maneja la lógica de negocio.</param>
        /// <returns>Retorna un código de estado 204 No Content si la eliminación es exitosa.</returns>
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarEstado([FromRoute] int id, [FromServices] IEstadoService estadoService)
        {
            await estadoService.EliminarEstadoPorId(id);
            return StatusCode(StatusCodes.Status204NoContent, ResponseApiService.Response(StatusCodes.Status204NoContent, "Estado eliminado"));
        }

        /// <summary>
        /// Obtiene un estado por su ID.
        /// </summary>
        /// <param name="id">El ID del estado que se va a obtener.</param>
        /// <param name="estadoService">El servicio de estados que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Estado si se encuentra, o un código de estado 404 Not Found si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadoPorId([FromRoute] int id, [FromServices] IEstadoService estadoService)
        {
            var estado = await estadoService.ObtenerEstadoPorId(id);
            if (estado == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, "Estado no encontrado"));
            }
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, estado));
        }

        /// <summary>
        /// Obtiene todos los estados registrados.
        /// </summary>
        /// <param name="estadoService">El servicio de estados que maneja la lógica de negocio.</param>
        /// <returns>Retorna una lista de todos los estados y un código de estado 200 OK.</returns>
        [HttpGet("Todos")]
        public async Task<IActionResult> ObtenerTodosLosEstados([FromServices] IEstadoService estadoService)
        {
            var estados = await estadoService.ObtenerTodosLosEstados();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, estados));
        }
    }
}

