using System;
using System.Collections.Generic;

namespace sweetbakery.Models;

public partial class Recetas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Ingredientes { get; set; }

    public string? Procedimiento { get; set; }

    public string? Reseña { get; set; }

    public int? IdCategoria { get; set; }

    public virtual Categorias? IdCategoriaNavigation { get; set; }
}
