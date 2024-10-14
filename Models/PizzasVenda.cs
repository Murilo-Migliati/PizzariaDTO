using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pizzaria.Models;

public partial class PizzasVenda
{
    [DisplayName("Código da Pizza")]
    public int IdPizza { get; set; }
    [DisplayName("Código da Venda")]
    public int IdVenda { get; set; }

    public int? Quantidade { get; set; }

    public double? Total { get; set; }
    [DisplayName("Código da Pizza")]
    public virtual Pizzas IdPizzaNavigation { get; set; } = null!;
    [DisplayName("Código da Venda")]
    public virtual Venda IdVendaNavigation { get; set; } = null!;
}
