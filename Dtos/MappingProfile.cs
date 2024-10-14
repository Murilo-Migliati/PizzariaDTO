using AutoMapper;
using Pizzaria.Models;
using Pizzaria.Dto;


public class MappingProfile: Profile
{
    public MappingProfile(){
        CreateMap<Cliente , ClienteDto>().ReverseMap();
        CreateMap<Pizzas , PizzasDto>().ReverseMap();
        CreateMap<PizzasVenda , PizzasVendaDto>().ReverseMap();
        CreateMap<Venda , VendaDto>().ReverseMap();
    }
}
