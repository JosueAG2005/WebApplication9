using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class PuntuacionesJurado
{
    public int IdPuntuacion { get; set; }

    public int? IdJurado { get; set; }

    public int? IdConcurso { get; set; }

    public int? IdInscripcion { get; set; }

    public int Puntuacion { get; set; }

    public string? Comentarios { get; set; }

    public string Razon { get; set; } = null!;

    public virtual Concurso? IdConcursoNavigation { get; set; }

    public virtual Inscripcione? IdInscripcionNavigation { get; set; }

    public virtual Jurado? IdJuradoNavigation { get; set; }
}
