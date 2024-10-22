using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoubleV.UsuarioRolesTareas.Domain.Entities;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(200)]
    public string Nombre { get; set; } = null!;

    [Column("contraseña")]
    [StringLength(50)]
    public string Contraseña { get; set; } = null!;

    [Column("email")]
    [StringLength(200)]
    public string Email { get; set; } = null!;

    [Column("rolId")]
    public int RolId { get; set; }

    [ForeignKey("RolId")]
    [InverseProperty("Usuarios")]
    public virtual Rol Rol { get; set; } = null!;

    [InverseProperty("Usuario")]
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
