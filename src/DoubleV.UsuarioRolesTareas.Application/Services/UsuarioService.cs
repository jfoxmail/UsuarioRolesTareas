using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DoubleV.UsuarioRolesTareas.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDVPDBContext _dataBaseContext;
        private readonly IMapper _mapper;
        private readonly IGetTokenJwtService _getTokenJwtService;

        public UsuarioService(IDVPDBContext dataBaseContext, IMapper mapper, IGetTokenJwtService getTokenJwtService)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
            _getTokenJwtService = getTokenJwtService;
        }

        public async Task<UsuarioModel> CrearUsuario(UsuarioModel usuario)
        {
            Usuario Usuario  = _mapper.Map<Usuario>(usuario);
            await _dataBaseContext.Usuarios.AddAsync(Usuario);

            try { 
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
            catch (DbUpdateException ex)
            {
                // Captura la excepción interna para obtener detalles más específicos
                var innerException = ex.InnerException?.Message;
                throw new Exception($"Error al guardar los cambios: {innerException}");
            }

            return _mapper.Map<UsuarioModel>(Usuario);
        }

        public async Task<UsuarioModel> ActualizarUsuario(UsuarioModel usuario)
        {
            Usuario Usuario = _mapper.Map<Usuario>(usuario);
            _dataBaseContext.Usuarios.Update(Usuario);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<UsuarioModel>(Usuario);
        }

        public async Task<UsuarioModel?> ObtenerUsuarioPorId(int id)
        {
            Usuario? Usuario = await _dataBaseContext.Usuarios.FindAsync(id);
            return Usuario != null ? _mapper.Map<UsuarioModel>(Usuario) : null;
        }

        public async Task<UsuarioAutenticadoModel?> ObtenerUsuarioPorEmailContraseña(UsuarioModel usuario)
        {
            Usuario? Usuario = await _dataBaseContext.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario.Email && x.Contraseña == usuario.Contraseña);            
            var UsuarioAutenticado = _mapper.Map<UsuarioAutenticadoModel>(Usuario);
            UsuarioAutenticado.Token = _getTokenJwtService.GetToken(UsuarioAutenticado.Id.ToString());
            return UsuarioAutenticado != null ? UsuarioAutenticado : null;
        }

        public async Task<List<UsuarioModel>> ObtenerTodosLosUsuarios()
        {
            var usuariosEntities = await _dataBaseContext.Usuarios.ToListAsync();
            return _mapper.Map<List<UsuarioModel>>(usuariosEntities);
        }

        public async Task EliminarUsuarioPorId(int id)
        {
            var Usuario = await _dataBaseContext.Usuarios.FindAsync(id);
            if (Usuario != null)
            {
                _dataBaseContext.Usuarios.Remove(Usuario);
                await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
        }

    }
}
