using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
using AutoMapper;
using Pizzaria.Dto;

namespace Pizzaria.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IGenericService<ClienteDto> _clienteService;

        public ClientesController(IGenericService<ClienteDto> clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = View(await _clienteService.GetAllAsync());
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteService.GetByIdAsync(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Nome")] ClienteDto clienteDto)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.AddAsync(clienteDto);
                return RedirectToAction(nameof(Index));
            }
            return View(clienteDto);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteService.GetByIdAsync(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cpf,Nome")] ClienteDto clienteDto)
        {
            if (id != clienteDto.Cpf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clienteService.UpdateAsync(clienteDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ClienteExists(clienteDto.Cpf))
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
            return View(clienteDto);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteService.GetByIdAsync(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _clienteService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClienteExists(int id)
        {
            // Retorna true se o cliente existir, ou false se não existir
            var cliente = await _clienteService.GetByIdAsync(id);
            return cliente != null;
        }
    }
}
