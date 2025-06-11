using System;
using System.Collections.Generic;

namespace GameCatalog.Models;

public partial class Disponibilidad
{
    public int DisponibilidadId { get; set; }

    public string NombreDisponibilidad { get; set; } = null!;

    public virtual ICollection<Juego> Juegos { get; set; } = new List<Juego>();
}
