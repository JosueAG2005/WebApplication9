using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Concurso
{
    public int IdConcurso { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string? Estado { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdTipoConcurso { get; set; }

    public virtual Categoría? IdCategoriaNavigation { get; set; }

    public virtual TiposDeConcurso? IdTipoConcursoNavigation { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<Jurado> Jurados { get; set; } = new List<Jurado>();

    public virtual ICollection<Premio> Premios { get; set; } = new List<Premio>();

    public virtual ICollection<PuntuacionesJurado> PuntuacionesJurados { get; set; } = new List<PuntuacionesJurado>();
}
