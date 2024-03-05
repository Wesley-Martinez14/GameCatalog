using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace GameCatalog.Models;

public partial class GeneroJuego
{
    public int GeneroId { get; set; }
    [DisplayName("Genero")]
    public string NombreGenero { get; set; } = null!;
    [DisplayName("Descripcion")]
    public string? DescripcionGenero { get; set; }

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
