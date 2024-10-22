using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleV.UsuarioRolesTareas.Application.Models
{
    public class TareaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int EstadoId { get; set; }
        public int UsuarioId { get; set; }
        
    }
}
