using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobilePhones.Models;

namespace MobilePhones.Controllers
{
    public class PhonesController : Controller
    {
        private readonly MobileContext _context;

        public PhonesController(MobileContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phones.ToListAsync());
        }

        // GET: Phones/Details/5
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

            return View(phone);
        }

        [HttpPost]
        public IActionResult Order(int phone_id, string user, string address, string phone)
        {
            var result = new object();
            if (phone_id != 0 && phone != null)
            {
                Order order = new Order() { 
                    PhoneId = phone_id, 
                    User = user, 
                    Address = address, 
                    ContactPhone = phone,
                    DateTime = DateTime.Now
                };
                _context.Add(order);
                _context.SaveChanges();
                result = new { message = "success" };
            } else
            {
                result = new { message = "error" };
            }

            return Ok(result);
        }


        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }
    }
}
