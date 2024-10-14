using System;
using System.Collections.Generic;
using Pizzaria.Models;

namespace Pizzaria.Dto;

public partial class PizzasDto
{
    public int IdPizza { get; set; }

    public string? Sabor { get; set; }

    public double? Preco { get; set; }

    public int? Quantidade { get; set; }

    public virtual ICollection<PizzasVenda> PizzasVenda { get; set; } = new List<PizzasVenda>();
}
