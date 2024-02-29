using System;
using System.Collections.Generic;

namespace GameCatalog.Models;

public partial class GeneroJuego
{
    public int GeneroId { get; set; }

    public string NombreGenero { get; set; } = null!;

    public string? DescripcionGenero { get; set; }

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
