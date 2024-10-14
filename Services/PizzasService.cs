using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pizzaria.Dto;
using Pizzaria.Models;
using Pizzaria.Repositories;

namespace Pizzaria.Services
{
    public class PizzasService : IGenericService<PizzasDto>
    {
        private readonly IGenericRepository<Pizzas> _pizzasRepository;

        private readonly IMapper _mapper;

        public PizzasService(IGenericRepository<Pizzas> pizzasRepository, IMapper mapper)
        {
            _pizzasRepository = pizzasRepository;
            _mapper = mapper;
        }

        public async Task<List<PizzasDto>> GetAllAsync()
        {
            var pizzas = await _pizzasRepository.GetAllAsync();
            return _mapper.Map<List<PizzasDto>>(pizzas);
        }

        public async Task<PizzasDto> GetByIdAsync(int id)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(id);
            return _mapper.Map<PizzasDto>(pizza);
        }

        public async Task AddAsync(PizzasDto pizzasDto)
        {
            var pizza = _mapper.Map<Pizzas>(pizzasDto);
            await _pizzasRepository.AddAsync(pizza);
        }

        public async Task UpdateAsync(PizzasDto pizzaDto)
        {
           var pizza = _mapper.Map<Pizzas>(pizzaDto);
           await _pizzasRepository.UpdateAsync(pizza);
        }

        public async Task DeleteAsync(int id)
        {
            await _pizzasRepository.DeleteAsync(id);
        }
    }
}
