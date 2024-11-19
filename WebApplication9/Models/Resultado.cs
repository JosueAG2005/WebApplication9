using System;
using System.Collections.Generic;

namespace WebApplication9.Models;

public partial class Resultado
{
    public int IdResultado { get; set; }

    public int? IdInscripcion { get; set; }

    public int PuntuacionFinal { get; set; }

    public int Clasificacion { get; set; }

    public virtual Inscripcione? IdInscripcionNavigation { get; set; }
}
