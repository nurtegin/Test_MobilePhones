using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using MobilePhones.Models;
using Microsoft.EntityFrameworkCore;

namespace MobilePhones.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly MobileContext _context;

        public HomeController(MobileContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var ordersGroups = _context.Orders
                .Include(o => o.Phone).ToList()
                .GroupBy(o => o.PhoneId).Select(g => g.ToList());

            var orders = _context.Orders.OrderBy(o => o.DateTime).ToList();
            orders = orders.Distinct().ToList();

            string[] dates = new string[orders.Count];
            DateTime[] dates2 = new DateTime[orders.Count];

            int c = 0;
            foreach(Order order in orders)
            {
                dates[c] = order.DateTime.ToString("dd-MM-yyyy");
                dates2[c] = order.DateTime;
                c++;
            }

            List<string> datesDistinct = dates.ToList().Distinct().ToList();
            List<DateTime> dates2Distinct = dates2.ToList().Distinct().ToList();

            var phonesData = new object[ordersGroups.Count()];

            int c2 = 0;
            foreach (var gr in ordersGroups)
            {
                string _label = gr[0].Phone.Name;

                int[] _data = new int[dates2Distinct.Count];

                int c3 = 0;
                _data[c3] = 0;
                List<Order> temp = new List<Order>();
                foreach (var date in dates2Distinct)
                {
                    var result = gr.Where(order => order.DateTime == date);
                    if(result != null)
                    {
                        foreach(var o in result)
                            _data[c3] += o.Phone.Price;
                    }
                    c3++;
                }
                phonesData[c2] = new { label = _label, data = _data };
                c2++;
            }

            var resultData = new { datesarray = datesDistinct, ordersdata = phonesData };
            ViewBag.PhoneOrders = JsonConvert.SerializeObject(resultData);
            return View();
        }
    }
}
