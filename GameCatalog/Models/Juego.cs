using System;
using System.Collections.Generic;

namespace GameCatalog.Models;

public partial class Juego
{
    public int JuegoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? GeneroJuegoId { get; set; }

    public int? ClasificacionJuegoId { get; set; }

    public int? EmpresaJuegoId { get; set; }

    public int? DisponibilidadJuegoId { get; set; }

    public DateTime? FechaEstreno { get; set; }

    public virtual ClasificacionJuego? ClasificacionJuego { get; set; }

    public virtual Disponibilidad? DisponibilidadJuego { get; set; }

    public virtual Empresa? EmpresaJuego { get; set; }

    public virtual GeneroJuego? GeneroJuego { get; set; }
}
