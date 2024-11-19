using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Inscripcione
{
    public int IdInscripcion { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdConcurso { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public virtual Concurso? IdConcursoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<PuntuacionesJurado> PuntuacionesJurados { get; set; } = new List<PuntuacionesJurado>();

    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
