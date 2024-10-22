using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.UsuarioRolesTareas.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IDVPDBContext _dataBaseContext;
        private readonly IMapper _mapper;

        public RolService(IDVPDBContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }

        public async Task<RolModel> CrearRol(RolModel rol)
        {
            Rol Rol = _mapper.Map<Rol>(rol);
            await _dataBaseContext.Rols.AddAsync(Rol);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<RolModel>(Rol);
        }

        public async Task<RolModel> ActualizarRol(RolModel rol)
        {
            Rol Rol = _mapper.Map<Rol>(rol);
            _dataBaseContext.Rols.Update(Rol);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<RolModel>(Rol);
        }

        public async Task<RolModel?> ObtenerRolPorId(int id)
        {
            Rol? Rol = await _dataBaseContext.Rols.FindAsync(id);
            return Rol != null ? _mapper.Map<RolModel>(Rol) : null;
        }

        public async Task<List<RolModel>> ObtenerTodosLosRoles()
        {
            var rolesEntities = await _dataBaseContext.Rols.ToListAsync();
            return _mapper.Map<List<RolModel>>(rolesEntities);
        }

        public async Task EliminarRolPorId(int id)
        {
            var Rol = await _dataBaseContext.Rols.FindAsync(id);
            if (Rol != null)
            {
                _dataBaseContext.Rols.Remove(Rol);
                await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}



