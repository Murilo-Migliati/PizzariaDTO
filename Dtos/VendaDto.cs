using System;
using System.Collections.Generic;
using Pizzaria.Models;

namespace Pizzaria.Dto;

public partial class VendaDto
{
    public int IdVenda { get; set; }

    public double? Total { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<PizzasVenda> PizzasVenda { get; set; } = new List<PizzasVenda>();
}
