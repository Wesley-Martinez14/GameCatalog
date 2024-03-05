using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameCatalog.Models;

public partial class Disponibilidad
{
    public int DisponibilidadId { get; set; }
    [DisplayName("Disponibilidades")]
    public string NombreDisponibilidad { get; set; } = null!;

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
