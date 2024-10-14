using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
using Pizzaria.Dto;


namespace Pizzaria.Controllers
{
    public class PizzasVendasController : Controller
    {
        private readonly IGenericService<PizzasVendaDto> _pizzasVendaService;

        public PizzasVendasController(IGenericService<PizzasVendaDto> pizzasVendaService)
        {
            _pizzasVendaService = pizzasVendaService;
        }

        // GET: PizzasVendas
        public async Task<IActionResult> Index()
        {
            var pizzasVendas = await _pizzasVendaService.GetAllAsync(); 
            return View(pizzasVendas);
        }

        // GET: PizzasVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzasVenda = await _pizzasVendaService.GetByIdAsync(id.Value);
            if (pizzasVenda == null)
            {
                return NotFound();
            }

            return View(pizzasVenda);
        }

        // GET: PizzasVendas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PizzasVendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPizza,IdVenda,Quantidade,Total")] PizzasVendaDto pizzasVendaDto)
        {
            if (ModelState.IsValid)
            {
                await _pizzasVendaService.AddAsync(pizzasVendaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(pizzasVendaDto);
        }

        // GET: PizzasVendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzasVenda = await _pizzasVendaService.GetByIdAsync(id.Value);
            if (pizzasVenda == null)
            {
                return NotFound();
            }
            return View(pizzasVenda);
        }

        // POST: PizzasVendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPizza,IdVenda,Quantidade,Total")] PizzasVendaDto pizzasVendaDto)
        {
            if (id != pizzasVendaDto.IdPizza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pizzasVendaService.UpdateAsync(pizzasVendaDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PizzasVendaExists(pizzasVendaDto.IdPizza))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pizzasVendaDto);
        }

        // GET: PizzasVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzasVenda = await _pizzasVendaService.GetByIdAsync(id.Value);
            if (pizzasVenda == null)
            {
                return NotFound();
            }

            return View(pizzasVenda);
        }

        // POST: PizzasVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pizzasVendaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PizzasVendaExists(int id)
        {
            var pizzasVenda = await _pizzasVendaService.GetByIdAsync(id);
            return pizzasVenda != null;
        }
    }
}






// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Pizzaria.Data;
// using Pizzaria.Models;

// namespace Pizzaria.Controllers
// {
//     public class PizzasVendasController : Controller
//     {
//         private readonly PizzariaContext _context;

//         public PizzasVendasController(PizzariaContext context)
//         {
//             _context = context;
//         }

//         // GET: PizzasVendas
//         public async Task<IActionResult> Index()
//         {
//             var pizzariaContext = _context.PizzasVenda.Include(p => p.IdPizzaNavigation).Include(p => p.IdVendaNavigation);
//             return View(await pizzariaContext.ToListAsync());
//         }

//         // GET: PizzasVendas/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var pizzasVenda = await _context.PizzasVenda
//                 .Include(p => p.IdPizzaNavigation)
//                 .Include(p => p.IdVendaNavigation)
//                 .FirstOrDefaultAsync(m => m.IdPizza == id);
//             if (pizzasVenda == null)
//             {
//                 return NotFound();
//             }

//             return View(pizzasVenda);
//         }

//         // GET: PizzasVendas/Create
//         public IActionResult Create()
//         {
//             ViewData["IdPizza"] = new SelectList(_context.Pizzas, "IdPizza", "IdPizza");
//             ViewData["IdVenda"] = new SelectList(_context.Venda, "IdVenda", "IdVenda");
//             return View();
//         }

//         // POST: PizzasVendas/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("IdPizza,IdVenda,Quantidade,Total")] PizzasVenda pizzasVenda)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(pizzasVenda);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["IdPizza"] = new SelectList(_context.Pizzas, "IdPizza", "IdPizza", pizzasVenda.IdPizza);
//             ViewData["IdVenda"] = new SelectList(_context.Venda, "IdVenda", "IdVenda", pizzasVenda.IdVenda);
//             return View(pizzasVenda);
//         }

//         // GET: PizzasVendas/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var pizzasVenda = await _context.PizzasVenda.FindAsync(id);
//             if (pizzasVenda == null)
//             {
//                 return NotFound();
//             }
//             ViewData["IdPizza"] = new SelectList(_context.Pizzas, "IdPizza", "IdPizza", pizzasVenda.IdPizza);
//             ViewData["IdVenda"] = new SelectList(_context.Venda, "IdVenda", "IdVenda", pizzasVenda.IdVenda);
//             return View(pizzasVenda);
//         }

//         // POST: PizzasVendas/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("IdPizza,IdVenda,Quantidade,Total")] PizzasVenda pizzasVenda)
//         {
//             if (id != pizzasVenda.IdPizza)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(pizzasVenda);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!PizzasVendaExists(pizzasVenda.IdPizza))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["IdPizza"] = new SelectList(_context.Pizzas, "IdPizza", "IdPizza", pizzasVenda.IdPizza);
//             ViewData["IdVenda"] = new SelectList(_context.Venda, "IdVenda", "IdVenda", pizzasVenda.IdVenda);
//             return View(pizzasVenda);
//         }

//         // GET: PizzasVendas/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var pizzasVenda = await _context.PizzasVenda
//                 .Include(p => p.IdPizzaNavigation)
//                 .Include(p => p.IdVendaNavigation)
//                 .FirstOrDefaultAsync(m => m.IdPizza == id);
//             if (pizzasVenda == null)
//             {
//                 return NotFound();
//             }

//             return View(pizzasVenda);
//         }

//         // POST: PizzasVendas/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var pizzasVenda = await _context.PizzasVenda.FindAsync(id);
//             if (pizzasVenda != null)
//             {
//                 _context.PizzasVenda.Remove(pizzasVenda);
//             }

//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool PizzasVendaExists(int id)
//         {
//             return _context.PizzasVenda.Any(e => e.IdPizza == id);
//         }
//     }
// }
