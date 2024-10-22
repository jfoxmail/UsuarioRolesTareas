using DoubleV.UsuarioRolesTareas.Application.Exceptions;
using DoubleV.UsuarioRolesTareas.Application.Features;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DoubleV.UsuarioRolesTareas.Api.Controllers
{
    [Authorize]
    [Route("api/v1/usuario")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="usuario">El objeto Usuario que contiene los datos del nuevo usuario.</param>
        /// <param name="usuarioService">El servicio de usuarios que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Usuario creado y un código de estado 201 Created.</returns>
        [AllowAnonymous]
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioModel usuario, [FromServices] IUsuarioService usuarioService, [FromServices] IValidator<UsuarioModel> validator)
        {
            var validate = await validator.ValidateAsync(usuario);
            if(!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));

            var data = await usuarioService.CrearUsuario(usuario);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="usuario">El objeto Usuario con los datos actualizados.</param>
        /// <param name="usuarioService">El servicio de usuarios que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Usuario actualizado y un código de estado 200 OK.</returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioModel usuario, [FromServices] IUsuarioService usuarioService, [FromServices] IValidator<UsuarioModel> validator)
        {
            var validate = await validator.ValidateAsync(usuario);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));
            var data = await usuarioService.ActualizarUsuario(usuario);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario que se va a eliminar.</param>
        /// <param name="usuarioService">El servicio de usuarios que maneja la lógica de negocio.</param>
        /// <returns>Retorna un código de estado 204 No Content si la eliminación es exitosa.</returns>
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] int id, [FromServices] IUsuarioService usuarioService)
        {
            await usuarioService.EliminarUsuarioPorId(id);
            return StatusCode(StatusCodes.Status204NoContent, ResponseApiService.Response(StatusCodes.Status204NoContent, "Usuario eliminado"));
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario que se va a obtener.</param>
        /// <param name="usuarioService">El servicio de usuarios que maneja la lógica de negocio.</param>
        /// <returns>Retorna el objeto Usuario si se encuentra, o un código de estado 404 Not Found si no se encuentra.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId([FromRoute] int id, [FromServices] IUsuarioService usuarioService)
        {
            var usuario = await usuarioService.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(StatusCodes.Status404NotFound, "Usuario no encontrado"));
            }
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, usuario));
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <param name="usuarioService">El servicio de usuarios que maneja la lógica de negocio.</param>
        /// <returns>Retorna una lista de todos los usuarios y un código de estado 200 OK.</returns>        
        [HttpGet("Todos")]
        public async Task<IActionResult> ObtenerTodosLosUsuarios([FromServices] IUsuarioService usuarioService)
        {
            var usuarios = await usuarioService.ObtenerTodosLosUsuarios();
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, usuarios));
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <param name="usuarioService">El servicio de usuarios que maneja la lógica de negocio.</param>
        /// <returns>Retorna una lista de todos los usuarios y un código de estado 200 OK.</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> ObtenerUsuarioEmailContraseña([FromBody] UsuarioModel usuario, [FromServices] IUsuarioService usuarioService)
        {
            var usuarios = await usuarioService.ObtenerUsuarioPorEmailContraseña(usuario);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(StatusCodes.Status200OK, usuarios));
        }
    }
}
