using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoubleV.UsuarioRolesTareas.Domain.Entities;

[Table("Pagina")]
public partial class Pagina
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("ruta")]
    [StringLength(100)]
    public string Ruta { get; set; } = null!;

    [Column("rolId")]
    public int RolId { get; set; }

    [ForeignKey("RolId")]
    [InverseProperty("Paginas")]
    public virtual Rol Rol { get; set; } = null!;
}
