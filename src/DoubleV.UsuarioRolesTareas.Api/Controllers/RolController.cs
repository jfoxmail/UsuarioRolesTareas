using DoubleV.UsuarioRolesTareas.Application.Exceptions;
using DoubleV.UsuarioRolesTareas.Application.Features;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.UsuarioRolesTareas.Api.Controllers
{
    [Route("api/v1/roles")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class RolController : ControllerBase
    {
        /// <summary>
        /// Crea un nuevo rol en el sistema.
        /// </summary>
        /// <param name="rol">El objeto Rol que contiene los datos del nuevo rol.</param>
        /// <param name="rolService">El servicio de roles que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Rol creado y un código de estado 201 Created.</returns>
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearRol([FromBody] RolModel rol, [FromServices] IRolService rolService)
        {
            var data = await rolService.CrearRol(rol);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        /// <summary>
        /// Actualiza los datos de un rol existente.
        /// </summary>
        /// <param name="rol">El objeto Rol con los datos actualizados.</param>
        /// <param name="rolService">El servicio de roles que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Rol actualizado y un código de estado 200 OK.</returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarRol([FromBody] RolModel rol, [FromServices] IRolService rolService)
        {
            var data = await rolService.ActualizarRol(rol);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Elimina un rol por su ID.
        /// </summary>
        /// <param name="id">El ID del rol que se va a eliminar.</param>
        /// <param name="rolService">El servicio de roles que maneja la lógica de negocio.</param>
        /// <returns>Retorna un código de estado 204 No Content si la eliminación es exitosa.</returns>
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarRol([FromRoute] int id, [FromServices] IRolService rolService)
        {
            await rolService.EliminarRolPorId(id);
            return StatusCode(StatusCodes.Status204NoContent, ResponseApiService.Response(StatusCodes.Status204NoContent, "Rol eliminado"));
        }

        /// <summary>
        /// Obtiene un rol por su ID.
        /// </summary>
        /// <param name="id">El ID del rol que se va a obtener.</param>
        /// <param name="rolService">El servicio de roles que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Rol si se encuentra, o un código de estado 404 Not Found si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRolPorId([FromRoute] int id, [FromServices] IRolService rolService)
        {
            var rol = await rolService.ObtenerRolPorId(id);
            if (rol == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, "Rol no encontrado"));
            }
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, rol));
        }

        /// <summary>
        /// Obtiene todos los roles registrados.
        /// </summary>
        /// <param name="rolService">El servicio de roles que maneja la lógica de negocio.</param>
        /// <returns>Retorna una lista de todos los roles y un código de estado 200 OK.</returns>
        [HttpGet("Todos")]
        public async Task<IActionResult> ObtenerTodosLosRoles([FromServices] IRolService rolService)
        {
            var roles = await rolService.ObtenerTodosLosRoles();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, roles));
        }
    }
}

