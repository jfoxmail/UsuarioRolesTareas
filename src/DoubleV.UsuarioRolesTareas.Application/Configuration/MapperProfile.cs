using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleV.UsuarioRolesTareas.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Usuario, UsuarioModel>().ReverseMap();
            CreateMap<Usuario, UsuarioAutenticadoModel>().ReverseMap();
            CreateMap<Rol, RolModel>().ReverseMap();
            CreateMap<Tarea, TareaModel>().ReverseMap();
            CreateMap<Pagina, PaginaModel>().ReverseMap();
            CreateMap<Estado, EstadoModel>().ReverseMap();



        }
    }
}
