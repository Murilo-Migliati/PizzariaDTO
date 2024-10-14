using System.Data;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories;

public class VendasRepository :IGenericRepository<Venda> 
{
    private readonly PizzariaContext _context;
    public VendasRepository(PizzariaContext context) {
        _context = context;
    }

    public async Task<List<Venda>> GetAllAsync(){
        return await _context.Venda.ToListAsync();
    }

    public async Task<Venda> GetByIdAsync(int id){
        return await _context.Venda.FindAsync(id);
    }

    public async Task AddAsync(Venda venda){
        await _context.AddAsync(venda);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Venda venda){
        _context.Venda.Update(venda);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        var venda = await _context.Venda.FindAsync(id);
        if(venda != null){
            _context.Venda.Remove(venda);
            await _context.SaveChangesAsync();
        }
    }
}
