using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models.Entities;
using InventoryManagement.Repositories;
using Microsoft.Build.Framework;
using NuGet.Protocol;

namespace InventoryManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork UoW;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            UoW = unitOfWork;
        }

        // GET: Products [done]
        public async Task<IActionResult> Index()
        {
            return View(await UoW.ProductRepository.GetAllAsync());
        }

        // GET: Products/Details/5 [done]
        public async Task<IActionResult> Details(Guid? PId)
        {
            var product = await UoW.ProductRepository.GetByIDAsync(PId);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: Products/Create [done]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create [done]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create([Bind("PId,PName,PAmount,UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                UoW.ProductRepository.Insert(product);
                await UoW.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5 [done]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var product = await UoW.ProductRepository.GetByIDAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5 [done]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PId,PName,PAmount,UnitPrice")] Product product)
        {
            if (id != product.PId) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    UoW.ProductRepository.Update(product);
                    await UoW.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UoW.ProductRepository.ProductExists(product.PId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5 [done]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var product = await UoW.ProductRepository.GetByIDAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5 [done]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await UoW.ProductRepository.GetByIDAsync(id);
            if (product != null)
            {
                UoW.ProductRepository.Delete(product);
            }
            await UoW.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/GetData
        // API that returns JSON for Tabulator.js Library
        public JsonResult GetData()
        {
            return Json(UoW.ProductRepository.GetAll());
        }

    }
}
