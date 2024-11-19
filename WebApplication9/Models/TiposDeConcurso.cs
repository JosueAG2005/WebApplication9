using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class TiposDeConcurso
{
    public int IdTipoConcurso { get; set; }

    public string NombreTipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Concurso> Concursos { get; set; } = new List<Concurso>();
}
