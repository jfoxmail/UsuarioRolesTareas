using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.UsuarioRolesTareas.Application.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IDVPDBContext _dataBaseContext;
        private readonly IMapper _mapper;

        public EstadoService(IDVPDBContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }

        public async Task<EstadoModel> CrearEstado(EstadoModel estado)
        {
            Estado Estado = _mapper.Map<Estado>(estado);
            await _dataBaseContext.Estados.AddAsync(Estado);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<EstadoModel>(Estado);
        }

        public async Task<EstadoModel> ActualizarEstado(EstadoModel estado)
        {
            Estado Estado = _mapper.Map<Estado>(estado);
            _dataBaseContext.Estados.Update(Estado);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<EstadoModel>(Estado);
        }

        public async Task<EstadoModel?> ObtenerEstadoPorId(int id)
        {
            Estado? Estado = await _dataBaseContext.Estados.FindAsync(id);
            return Estado != null ? _mapper.Map<EstadoModel>(Estado) : null;
        }

        public async Task<List<EstadoModel>> ObtenerTodosLosEstados()
        {
            var estadosEntities = await _dataBaseContext.Estados.ToListAsync();
            return _mapper.Map<List<EstadoModel>>(estadosEntities);
        }

        public async Task EliminarEstadoPorId(int id)
        {
            var Estado = await _dataBaseContext.Estados.FindAsync(id);
            if (Estado != null)
            {
                _dataBaseContext.Estados.Remove(Estado);
                await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}

