using System.Data;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories;

public class PizzasRepository :IGenericRepository<Pizzas> 
{
    private readonly PizzariaContext _context;
    public PizzasRepository(PizzariaContext context) {
        _context = context;
    }

    public async Task<List<Pizzas>> GetAllAsync(){
        return await _context.Pizzas.ToListAsync();
    }

    public async Task<Pizzas> GetByIdAsync(int id){
        return await _context.Pizzas.FindAsync(id);
    }

    public async Task AddAsync(Pizzas pizza){
        await _context.AddAsync(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pizzas pizza){
        _context.Pizzas.Update(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id){
        var procuraPizza = await _context.Pizzas.FindAsync(id);
        if(procuraPizza !=null){
            _context.Pizzas.Remove(procuraPizza);
            await _context.SaveChangesAsync();
        }
    }
}
