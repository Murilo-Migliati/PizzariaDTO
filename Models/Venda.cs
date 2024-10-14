using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pizzaria.Models;

public partial class Venda
{
    [DisplayName("Código da Venda")]
    public int IdVenda { get; set; }

    public double? Total { get; set; }

    [DisplayName("Código do cliente")]
    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual ICollection<PizzasVenda> PizzasVenda { get; set; } = new List<PizzasVenda>();
}
