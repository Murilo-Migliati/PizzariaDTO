using System;
using System.Collections.Generic;
using System.ComponentModel;
using Pizzaria.Models;

namespace Pizzaria.Dto;

public partial class ClienteDto
{
    [DisplayName("CPF")]
    public int Cpf { get; set; }
    [DisplayName("Nome do Cliente")]
    public string? Nome { get; set; }

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}
