using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoubleV.UsuarioRolesTareas.Domain.Entities;

[Table("Estado")]
public partial class Estado
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Estado")]
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
