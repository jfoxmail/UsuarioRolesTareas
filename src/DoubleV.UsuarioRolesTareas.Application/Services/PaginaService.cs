using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.UsuarioRolesTareas.Application.Services
{
    public class PaginaService : IPaginaService
    {
        private readonly IDVPDBContext _dataBaseContext;
        private readonly IMapper _mapper;

        public PaginaService(IDVPDBContext dataBaseContext, IMapper mapper)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
        }

        public async Task<PaginaModel> CrearPagina(PaginaModel pagina)
        {
            Pagina Pagina = _mapper.Map<Pagina>(pagina);
            await _dataBaseContext.Paginas.AddAsync(Pagina);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<PaginaModel>(Pagina);
        }

        public async Task<PaginaModel> ActualizarPagina(PaginaModel pagina)
        {
            Pagina Pagina = _mapper.Map<Pagina>(pagina);
            _dataBaseContext.Paginas.Update(Pagina);
            await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<PaginaModel>(Pagina);
        }

        public async Task<PaginaModel?> ObtenerPaginaPorId(int id)
        {
            Pagina? Pagina = await _dataBaseContext.Paginas.FindAsync(id);
            return Pagina != null ? _mapper.Map<PaginaModel>(Pagina) : null;
        }

        public async Task<List<PaginaModel>> ObtenerTodasLasPaginas()
        {
            var paginasEntities = await _dataBaseContext.Paginas.ToListAsync();
            return _mapper.Map<List<PaginaModel>>(paginasEntities);
        }

        public async Task EliminarPaginaPorId(int id)
        {
            var Pagina = await _dataBaseContext.Paginas.FindAsync(id);
            if (Pagina != null)
            {
                _dataBaseContext.Paginas.Remove(Pagina);
                await _dataBaseContext.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}

