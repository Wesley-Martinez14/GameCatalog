using System;
using System.Collections.Generic;

namespace GameCatalog.Models;

public partial class ClasificacionJuego
{
    public int ClasificacionId { get; set; }

    public string NombreClasificacion { get; set; } = null!;

    public string? DescripcionClasificacion { get; set; }

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
