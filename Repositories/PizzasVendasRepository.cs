using System.Data;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories;

public class PizzasVendasRepository :IGenericRepository<PizzasVenda> 
{
    private readonly PizzariaContext _context;
    public PizzasVendasRepository(PizzariaContext context) {
        _context = context;
    }

    public async Task<List<PizzasVenda>> GetAllAsync(){
        return await _context.PizzasVenda.ToListAsync(); 
    }

    public async Task<PizzasVenda> GetByIdAsync(int id){
        return await _context.PizzasVenda.FindAsync(id);
    }

    public async Task AddAsync(PizzasVenda pizzasVenda){
        await _context.AddAsync(pizzasVenda);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PizzasVenda pizzasVenda){
        _context.PizzasVenda.Update(pizzasVenda);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        var pizzasVenda = await _context.PizzasVenda.FindAsync(id);
        if(pizzasVenda != null){
            _context.PizzasVenda.Remove(pizzasVenda);
            await _context.SaveChangesAsync();
        }
    }
}
