using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pizzaria.Models;
using Pizzaria.Repositories;
using Pizzaria.Dto;

namespace Pizzaria.Services
{
    public class VendasService : IGenericService<VendaDto>
    {
        private readonly IGenericRepository<Venda> _vendasRepository;
        private readonly IMapper _mapper;

        public VendasService(IGenericRepository<Venda> vendasRepository, IMapper mapper)
        {
            _vendasRepository = vendasRepository;
            _mapper = mapper;
        }

        public async Task<List<VendaDto>> GetAllAsync()
        {
            var clientes = await _vendasRepository.GetAllAsync();
            return _mapper.Map<List<VendaDto>>(clientes);
        }

        public async Task<VendaDto> GetByIdAsync(int id)
        {
            var cliente = await _vendasRepository.GetByIdAsync(id);
            return _mapper.Map<VendaDto>(cliente);
        }

        public async Task AddAsync(VendaDto vendaDto)
        {
            var venda = _mapper.Map<Venda>(vendaDto);
            await _vendasRepository.AddAsync(venda);
        }

        public async Task UpdateAsync(VendaDto vendaDto)
        {
            var venda = _mapper.Map<Venda>(vendaDto);
            await _vendasRepository.UpdateAsync(venda);
        }

        public async Task DeleteAsync(int id)
        {
            await _vendasRepository.DeleteAsync(id);
        }
    }
}
