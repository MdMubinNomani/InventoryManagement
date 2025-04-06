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
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Products [done]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ProductRepository.GetAllAsync());
        }

        // GET: Products/Details/5 [done]
        public async Task<IActionResult> Details(int PId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIDAsync(PId);
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
                _unitOfWork.ProductRepository.Insert(product);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5 [done]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            var product = await _unitOfWork.ProductRepository.GetByIDAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5 [done]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PName,PAmount,UnitPrice")] Product product)
        {
            if (id != product.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ProductRepository.Update(product);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.ProductRepository.ProductExists(product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5 [done]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();
            var product = await _unitOfWork.ProductRepository.GetByIDAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5 [done]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIDAsync(id);
            if (product != null)
            {
                _unitOfWork.ProductRepository.Delete(product);
            }
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/GetData
        // API that returns JSON for Tabulator.js Library

        public JsonResult GetData()
        {
            var data = _unitOfWork.ProductRepository.GetAll();
            return Json(data);
        }

        // GET: Products/SearchRecords
        // for select2 implementation

        public JsonResult SearchRecords(string term)
        {
            var results = _unitOfWork.ProductRepository.GetByName(term)
                .Select(p => new { id = p.Id, text = p.PName })
                .ToList();

            return Json(results);
        }
    }
}
