using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameCatalog.Models;

public partial class Juego
{
    public int JuegoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
    [DisplayName("Genero")]
    public int? GeneroJuegoId { get; set; }
    [DisplayName("Clasificacion")]
    public int? ClasificacionJuegoId { get; set; }
    [DisplayName("Empresa")]
    public int? EmpresaJuegoId { get; set; }
    [DisplayName("Disponibilidad")]
    public int? DisponibilidadJuegoId { get; set; }
    [DisplayName("Fecha Estreno")]
    public DateTime? FechaEstreno { get; set; }

    public virtual ClasificacionJuego? ClasificacionJuego { get; set; }

    public virtual Disponibilidad? DisponibilidadJuego { get; set; }

    public virtual Empresa? EmpresaJuego { get; set; }

    public virtual GeneroJuego? GeneroJuego { get; set; }
}
