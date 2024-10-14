using System.Data;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories;

public class ClienteRepository :IGenericRepository<Cliente> 
{
    private readonly PizzariaContext _context;
    public ClienteRepository(PizzariaContext context) {
        _context = context;
    }

    public async Task<List<Cliente>> GetAllAsync(){
        return await _context.Cliente.ToListAsync(); 
    }

    public async Task<Cliente> GetByIdAsync(int id){
        return await _context.Cliente.FindAsync(id);
    }

    public async Task AddAsync(Cliente cliente){
        await _context.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cliente cliente){
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        var procuraCliente = await _context.Cliente.FindAsync(id);
        if(procuraCliente != null){
            _context.Cliente.Remove(procuraCliente);
            await _context.SaveChangesAsync();
        }
    }
}
