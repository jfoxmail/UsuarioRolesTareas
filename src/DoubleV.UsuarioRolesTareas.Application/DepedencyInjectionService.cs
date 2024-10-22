using AutoMapper;
using DoubleV.UsuarioRolesTareas.Application.Configuration;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Application.Models;
using DoubleV.UsuarioRolesTareas.Application.Services;
using DoubleV.UsuarioRolesTareas.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleV.UsuarioRolesTareas.Application
{
    public static class DepedencyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });
            services.AddSingleton(mapper.CreateMapper());
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IRolService, RolService>();
            services.AddTransient<IEstadoService, EstadoService>();
            services.AddTransient<IPaginaService, PaginaService>();
            services.AddTransient<ITareaService, TareaService>();

            services.AddScoped<IValidator<UsuarioModel>,UsuarioValidator>();

            return services;

        }
    }
}
