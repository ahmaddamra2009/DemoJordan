﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoJordan.Data;
using DemoJordan.Models;

namespace DemoJordan.Controllers
{
    public class CitizensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitizensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
        {
              return _context.Citizens != null ? 
                          View(await _context.Citizens.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Citizens'  is null.");
        }

        // GET: Citizens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Citizens == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens
                .FirstOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Citizens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenId,DBRNumber,CitizenName,Gender,BirthType,NumberOfBorn,BOD,BODText,FatherName,FatherAge,FatherWork,MotherName,City,Side,Eliminate,Governorate,DepartmentOfHealth,QRCode")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                citizen.CitizenId = Guid.NewGuid();
                _context.Add(citizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        // GET: Citizens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Citizens == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Citizens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CitizenId,DBRNumber,CitizenName,Gender,BirthType,NumberOfBorn,BOD,BODText,FatherName,FatherAge,FatherWork,MotherName,City,Side,Eliminate,Governorate,DepartmentOfHealth,QRCode")] Citizen citizen)
        {
            if (id != citizen.CitizenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenExists(citizen.CitizenId))
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
            return View(citizen);
        }

        // GET: Citizens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Citizens == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens
                .FirstOrDefaultAsync(m => m.CitizenId == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Citizens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Citizens'  is null.");
            }
            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen != null)
            {
                _context.Citizens.Remove(citizen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(Guid id)
        {
          return (_context.Citizens?.Any(e => e.CitizenId == id)).GetValueOrDefault();
        }
    }
}
