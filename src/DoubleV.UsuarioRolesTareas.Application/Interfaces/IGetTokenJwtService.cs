using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleV.UsuarioRolesTareas.Application.Interfaces
{
    public interface IGetTokenJwtService
    {
        string GetToken(string id);
    }
}
