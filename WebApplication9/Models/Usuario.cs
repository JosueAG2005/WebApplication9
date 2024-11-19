using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string? TipoUsuario { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<Jurado> Jurados { get; set; } = new List<Jurado>();
}
