using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.UsuarioRolesTareas.Application.Services
{
    public class TareaService : ITareaService
    {
        private readonly IDVPDBContext _dataBaseContext;
        private readonly IMapper _mapper;

        public TareaService(IDVPDBContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }

        public async Task<TareaModel> CrearTarea(TareaModel tarea)
        {
            Tarea Tarea = _mapper.Map<Tarea>(tarea);
            await _dataBaseContext.Tareas.AddAsync(Tarea);
            try
            {
                await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
            catch (DbUpdateException ex)
            {
                // Captura la excepción interna para obtener detalles más específicos
                var innerException = ex.InnerException?.Message;
                throw new Exception($"Error al guardar los cambios: {innerException}");
            }
            return _mapper.Map<TareaModel>(Tarea);
        }

        public async Task<TareaModel> ActualizarTarea(TareaModel tarea)
        {
            Tarea Tarea = _mapper.Map<Tarea>(tarea);
            _dataBaseContext.Tareas.Update(Tarea);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<TareaModel>(Tarea);
        }

        public async Task<TareaModel?> ObtenerTareaPorId(int id)
        {
            Tarea? Tarea = await _dataBaseContext.Tareas.FindAsync(id);
            return Tarea != null ? _mapper.Map<TareaModel>(Tarea) : null;
        }

        public async Task<List<TareaModel>> ObtenerTodasLasTareas()
        {
            var tareasEntities = await _dataBaseContext.Tareas.ToListAsync();
            return _mapper.Map<List<TareaModel>>(tareasEntities);
        }

        public async Task<bool> EliminarTareaPorId(int id)
        {
            var Tarea = await _dataBaseContext.Tareas.FindAsync(id);
            if (Tarea != null)
            {
                _dataBaseContext.Tareas.Remove(Tarea);
                await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
            return true;
        }
    }
}

