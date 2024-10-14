using AutoMapper;
using NuGet.Configuration;
using Pizzaria.Models;
using Pizzaria.Dto;

namespace Pizzaria.Services
{
    public class PizzasVendasService : IGenericService<PizzasVendaDto>
    {
        private readonly IGenericRepository<PizzasVenda> _pizzasVendasRepository;
        private readonly IMapper _mapper;

        public PizzasVendasService(IGenericRepository<PizzasVenda> pizzasVendasRepository, IMapper mapper)
        {
            _pizzasVendasRepository = pizzasVendasRepository;
            _mapper = mapper;
        }

        public async Task<List<PizzasVendaDto>> GetAllAsync()
        {
            var pizzasVendas = await _pizzasVendasRepository.GetAllAsync();
            return _mapper.Map<List<PizzasVendaDto>>(pizzasVendas);
        }


        public async Task<PizzasVendaDto> GetByIdAsync(int id)
        {
            var pizzasVenda = await _pizzasVendasRepository.GetByIdAsync(id);
            return _mapper.Map<PizzasVendaDto>(pizzasVenda);
        }

        public async Task AddAsync(PizzasVendaDto pizzasVendaDto)
        {
            var pizzasVenda = _mapper.Map<PizzasVenda>(pizzasVendaDto);
            await _pizzasVendasRepository.AddAsync(pizzasVenda);
        }

        public async Task UpdateAsync(PizzasVendaDto pizzasVendaDto)
        {
            var pizzasVenda = _mapper.Map<PizzasVenda>(pizzasVendaDto);
            await _pizzasVendasRepository.UpdateAsync(pizzasVenda);
        }

        public async Task DeleteAsync(int id)
        {
            await _pizzasVendasRepository.DeleteAsync(id);
        }
    }
}
