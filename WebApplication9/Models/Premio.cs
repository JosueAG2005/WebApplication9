using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Premio
{
    public int IdPremio { get; set; }

    public int? IdConcurso { get; set; }

    public string? Descripcion { get; set; }

    public int Cantidad { get; set; }

    public virtual Concurso? IdConcursoNavigation { get; set; }
}
