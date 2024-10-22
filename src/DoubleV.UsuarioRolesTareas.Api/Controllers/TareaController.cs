using DoubleV.UsuarioRolesTareas.Application.Exceptions;
using DoubleV.UsuarioRolesTareas.Application.Features;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.UsuarioRolesTareas.Api.Controllers
{
    [Route("api/v1/tareas")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class TareaController : ControllerBase
    {
        /// <summary>
        /// Crea una nueva tarea en el sistema.
        /// </summary>
        /// <param name="tarea">El objeto Tarea que contiene los datos de la nueva tarea.</param>
        /// <param name="tareaService">El servicio de tareas que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Tarea creado y un código de estado 201 Created.</returns>
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearTarea([FromBody] TareaModel tarea, [FromServices] ITareaService tareasService)
        {
            var data = await tareasService.CrearTarea(tarea);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        /// <summary>
        /// Actualiza los datos de una tarea existente.
        /// </summary>
        /// <param name="tarea">El objeto Tarea con los datos actualizados.</param>
        /// <param name="tareaService">El servicio de tareas que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Tarea actualizado y un código de estado 200 OK.</returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarTarea([FromBody] TareaModel tarea, [FromServices] ITareaService tareasService)
        {
            var data = await tareasService.ActualizarTarea(tarea);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Elimina una tarea por su ID.
        /// </summary>
        /// <param name="id">El ID de la tarea que se va a eliminar.</param>
        /// <param name="tareaService">El servicio de tareas que maneja la lógica de negocio.</param>
        /// <returns>Retorna un código de estado 204 No Content si la eliminación es exitosa.</returns>
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarTarea([FromRoute] int id, [FromServices] ITareaService tareasService)
        {
            if(id == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest));
            var data = await tareasService.EliminarTareaPorId(id);
            if(!data)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, data));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, "Tarea eliminada"));
        }

        /// <summary>
        /// Obtiene una tarea por su ID.
        /// </summary>
        /// <param name="id">El ID de la tarea que se va a obtener.</param>
        /// <param name="tareaService">El servicio de tareas que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Tarea si se encuentra, o un código de estado 404 Not Found si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTareaPorId([FromRoute] int id, [FromServices] ITareaService tareasService)
        {
            var tarea = await tareasService.ObtenerTareaPorId(id);
            if (tarea == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, "Tarea no encontrada"));
            }
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, tarea));
        }

        /// <summary>
        /// Obtiene todas las tareas registradas.
        /// </summary>
        /// <param name="tareaService">El servicio de tareas que maneja la lógica de negocio.</param>
        /// <returns>Retorna una lista de todas las tareas y un código de estado 200 OK.</returns>
        [HttpGet("Todas")]
        public async Task<IActionResult> ObtenerTodasLasTareas([FromServices] ITareaService tareasService)
        {
            var tareas = await tareasService.ObtenerTodasLasTareas();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, tareas));
        }
    }
}

