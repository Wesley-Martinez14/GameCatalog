using System;
using System.Collections.Generic;

namespace GameCatalog.Models;

public partial class Empresa
{
    public int EmpresaId { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public string? InfoEmpresa { get; set; }

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
