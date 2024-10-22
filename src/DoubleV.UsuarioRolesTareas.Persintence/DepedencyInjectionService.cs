
using DoubleV.UsuarioRolesTareas.Application.DataBase;
using DoubleV.UsuarioRolesTareas.Application.Interfaces;
using DoubleV.UsuarioRolesTareas.Persintence.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleV.UsuarioRolesTareas.Persintence
{
    public static class DepedencyInjectionService
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<IDVPDBContext, DVPDBContext>(options => options.UseSqlServer(configuration["SQLConnectionString"]));
            services.AddSingleton<IGetTokenJwtService, GetTokenJwtService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyJwt"])),
                    ValidIssuer = configuration["IssuerJwt"],
                    ValidAudience = configuration["AudienceJwt"]
                };

            });
            return services;

        }
    }
}
