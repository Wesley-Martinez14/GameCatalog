using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameCatalog.Models;

public partial class ClasificacionJuego
{
    public int ClasificacionId { get; set; }
    [DisplayName("Clasificacion")]
    public string NombreClasificacion { get; set; } = null!;
    [DisplayName("Descripcion")]
    public string? DescripcionClasificacion { get; set; }

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
