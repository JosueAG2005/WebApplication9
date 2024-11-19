using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Jurado
{
    public int IdJurado { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdConcurso { get; set; }

    public virtual Concurso? IdConcursoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<PuntuacionesJurado> PuntuacionesJurados { get; set; } = new List<PuntuacionesJurado>();
}
