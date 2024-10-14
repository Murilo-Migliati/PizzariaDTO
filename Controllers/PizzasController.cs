using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
using Pizzaria.Services;
using Pizzaria.Dto;

namespace Pizzaria.Controllers
{
    public class PizzasController : Controller
    {
        private readonly IGenericService<PizzasDto> _pizzasService;

        public PizzasController(IGenericService<PizzasDto> pizzasService)
        {
            _pizzasService = pizzasService;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            var pizzas = View(await _pizzasService.GetAllAsync()); // Chama o serviço para obter as pizzas
            return View(pizzas);
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzasService.GetByIdAsync(id.Value); // Chama o serviço para obter a pizza
            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPizza,Sabor,Preco,Quantidade")] PizzasDto pizzasDto)
        {
            if (ModelState.IsValid)
            {
                await _pizzasService.AddAsync(pizzasDto); // Chama o serviço para adicionar a pizza
                return RedirectToAction(nameof(Index));
            }
            return View(pizzasDto);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzasService.GetByIdAsync(id.Value); // Chama o serviço para obter a pizza
            if (pizzas == null)
            {
                return NotFound();
            }
            return View(pizzas);
        }

        // POST: Pizzas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPizza,Sabor,Preco,Quantidade")] PizzasDto pizzasDto)
        {
            if (id != pizzasDto.IdPizza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pizzasService.UpdateAsync(pizzasDto); // Chama o serviço para atualizar a pizza
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PizzasExistsAsync(pizzasDto.IdPizza)) // Verifica se a pizza existe
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
            return View(pizzasDto);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzasService.GetByIdAsync(id.Value); // Chama o serviço para obter a pizza
            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizzas = await _pizzasService.GetByIdAsync(id); // Chama o serviço para obter a pizza
            if (pizzas != null)
            {
                await _pizzasService.DeleteAsync(id); // Chama o serviço para remover a pizza
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PizzasExistsAsync(int id) // Método auxiliar para verificar a existência da pizza
        {
            return await _pizzasService.GetByIdAsync(id) != null;
        }
    }
}
