using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoubleV.UsuarioRolesTareas.Domain.Entities;

[Table("Tarea")]
public partial class Tarea
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(200)]
    public string Nombre { get; set; } = null!;

    [Column("estadoId")]
    public int EstadoId { get; set; }

    [Column("usuarioId")]
    public int UsuarioId { get; set; }

    [ForeignKey("EstadoId")]
    [InverseProperty("Tareas")]
    public virtual Estado Estado { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("Tareas")]
    public virtual Usuario Usuario { get; set; } = null!;
}
