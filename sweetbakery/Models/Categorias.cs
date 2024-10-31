using System;
using System.Collections.Generic;

namespace sweetbakery.Models;

public partial class Categorias
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();

    public virtual ICollection<Recetas> Recetas { get; set; } = new List<Recetas>();
}
