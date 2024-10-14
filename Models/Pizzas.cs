using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pizzaria.Models;

public partial class Pizzas
{
    [DisplayName("Código da Pizza")]
    public int IdPizza { get; set; }

    public string? Sabor { get; set; }
    [DisplayName("Preço")]
    public double? Preco { get; set; }

    public int? Quantidade { get; set; }

    public virtual ICollection<PizzasVenda> PizzasVenda { get; set; } = new List<PizzasVenda>();
}
