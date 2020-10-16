using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobilePhones.Models;

namespace MobilePhones.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PhonesController : Controller
    {
        private readonly MobileContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhonesController(MobileContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Phones
        public async Task<IActionResult> Index(string search)
        {
            List<Phone> phones;
            if (search != null)
            {
                phones = await _context.Phones.Where(p => p.Name.Contains(search)).ToListAsync();
            }
            else
            {
                phones = await _context.Phones.ToListAsync();
            }
            return View(phones);
        }

        // GET: Admin/Phones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }
            phone.Comments = await _context.Comments.Include("User").Where(c => c.PhoneId == phone.Id).ToListAsync();
            return View(phone);
        }

        // GET: Admin/Phones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Company,Price,ImageFile")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                if(phone.ImageFile != null)
                {
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "phone_images");
                    string fileName = $"{Guid.NewGuid().ToString()}_{phone.ImageFile.FileName}";
                    string filePath = Path.Combine(folderPath, fileName);

                    using(var stream = new FileStream(filePath, FileMode.Create))
                    {
                        phone.ImageFile.CopyTo(stream);
                    }

                    phone.ImageName = fileName;
                }
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phone);
        }

        // GET: Admin/Phones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        // POST: Admin/Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Company,Price,ImageFile")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (phone.ImageFile != null)
                    {
                        string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "phone_images");
                        string fileName = $"{Guid.NewGuid().ToString()}_{phone.ImageFile.FileName}";
                        string filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            phone.ImageFile.CopyTo(stream);
                        }

                        phone.ImageName = fileName;
                    }
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
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
            return View(phone);
        }

        // GET: Admin/Phones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Admin/Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = comment.PhoneId });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var phones = await _context.Phones.ToListAsync();
                var data = phones.Where(a => a.Name.Contains(term, StringComparison.OrdinalIgnoreCase)
                || a.Company.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList().AsReadOnly();
                return Ok(data);
            }
            else
            {
                return Ok();
            }
        }

        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }
    }
}
