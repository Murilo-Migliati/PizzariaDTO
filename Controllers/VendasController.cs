using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Dto;
namespace Pizzaria.Controllers
{
    public class VendasController : Controller
    {
        private readonly IGenericService<VendaDto> _vendaService;

        public VendasController(IGenericService<VendaDto> vendaService)
        {
            _vendaService = vendaService;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var vendas = await _vendaService.GetAllAsync(); // Chama o serviço para obter as vendas
            return View(vendas);
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _vendaService.GetByIdAsync(id.Value);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Total,IdCliente")] VendaDto vendaDto)
        {
            if (ModelState.IsValid)
            {
                await _vendaService.AddAsync(vendaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(vendaDto);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _vendaService.GetByIdAsync(id.Value);
            if (venda == null)
            {
                return NotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenda,Total,IdCliente")] VendaDto vendaDto)
        {
            if (id != vendaDto.IdVenda) {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vendaService.UpdateAsync(vendaDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VendaExists(vendaDto.IdVenda))
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
            return View(vendaDto);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _vendaService.GetByIdAsync(id.Value);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vendaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> VendaExists(int id)
        {
            var venda = await _vendaService.GetByIdAsync(id);
            return venda != null;
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
//     public class VendasController : Controller
//     {
//         private readonly PizzariaContext _context;

//         public VendasController(PizzariaContext context)
//         {
//             _context = context;
//         }

//         // GET: Vendas
//         public async Task<IActionResult> Index()
//         {
//             var pizzariaContext = _context.Venda.Include(v => v.IdClienteNavigation);
//             return View(await pizzariaContext.ToListAsync());
//         }

//         // GET: Vendas/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var venda = await _context.Venda
//                 .Include(v => v.IdClienteNavigation)
//                 .FirstOrDefaultAsync(m => m.IdVenda == id);
//             if (venda == null)
//             {
//                 return NotFound();
//             }

//             return View(venda);
//         }

//         // GET: Vendas/Create
//         public IActionResult Create()
//         {
//             ViewData["IdCliente"] = new SelectList(_context.Cliente, "Cpf", "Cpf");
//             return View();
//         }

//         // POST: Vendas/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("IdVenda,Total,IdCliente")] Venda venda)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(venda);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["IdCliente"] = new SelectList(_context.Cliente, "Cpf", "Cpf", venda.IdCliente);
//             return View(venda);
//         }

//         // GET: Vendas/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var venda = await _context.Venda.FindAsync(id);
//             if (venda == null)
//             {
//                 return NotFound();
//             }
//             ViewData["IdCliente"] = new SelectList(_context.Cliente, "Cpf", "Cpf", venda.IdCliente);
//             return View(venda);
//         }

//         // POST: Vendas/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("IdVenda,Total,IdCliente")] Venda venda)
//         {
//             if (id != venda.IdVenda)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(venda);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!VendaExists(venda.IdVenda))
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
//             ViewData["IdCliente"] = new SelectList(_context.Cliente, "Cpf", "Cpf", venda.IdCliente);
//             return View(venda);
//         }

//         // GET: Vendas/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var venda = await _context.Venda
//                 .Include(v => v.IdClienteNavigation)
//                 .FirstOrDefaultAsync(m => m.IdVenda == id);
//             if (venda == null)
//             {
//                 return NotFound();
//             }

//             return View(venda);
//         }

//         // POST: Vendas/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var venda = await _context.Venda.FindAsync(id);
//             if (venda != null)
//             {
//                 _context.Venda.Remove(venda);
//             }

//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool VendaExists(int id)
//         {
//             return _context.Venda.Any(e => e.IdVenda == id);
//         }
//     }
// }
