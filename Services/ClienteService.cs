using AutoMapper;
using Pizzaria.Models;
using Pizzaria.Dto;
namespace Pizzaria.Services
{
    public class ClienteService : IGenericService<ClienteDto>
    {
        private readonly IGenericRepository<Cliente> _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return _mapper.Map<List<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var  cliente = await _clienteRepository.GetByIdAsync(id);   
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task AddAsync(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task UpdateAsync(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }
    }
}
