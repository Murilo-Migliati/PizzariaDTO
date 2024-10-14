using System;
using System.Collections.Generic;
using Pizzaria.Models;

namespace Pizzaria.Dto;

public partial class PizzasVendaDto
{
    public int IdPizza { get; set; }

    public int IdVenda { get; set; }

    public int? Quantidade { get; set; }

    public double? Total { get; set; }

    public virtual Pizzas IdPizzaNavigation { get; set; } = null!;

    public virtual Venda IdVendaNavigation { get; set; } = null!;
}
