using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameCatalog.Models;

public partial class Empresa
{
    public int EmpresaId { get; set; }
    [DisplayName("Nombre")]
    public string NombreEmpresa { get; set; } = null!;
    [DisplayName("Informacion de la empresa")]
    public string? InfoEmpresa { get; set; }

    public virtual ICollection<Juego> Juegos { get; } = new List<Juego>();
}
