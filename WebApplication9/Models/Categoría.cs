using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Categoría
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Concurso> Concursos { get; set; } = new List<Concurso>();
}
