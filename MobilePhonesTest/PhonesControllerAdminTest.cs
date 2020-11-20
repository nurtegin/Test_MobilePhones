using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhones.Areas.Admin.Controllers;
using MobilePhones.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobilePhonesTest
{
    public class PhonesControllerAdminTest
    {
        private PhonesController _phonesController;
        private MobileContext _context;
        private IWebHostEnvironment _ihost;

        public PhonesControllerAdminTest()
        {
            var webhostmocker = new Mock<IWebHostEnvironment>();
            _phonesController = new PhonesController(_context, webhostmocker.Object);
            _ihost = webhostmocker.Object;
            var options = new DbContextOptionsBuilder<MobileContext>()
                                     .UseInMemoryDatabase(databaseName: "phonesdb")
                                     .Options;


        }
        [Fact]
        public async Task IndexActionSearchPhones()
        {
            var searchString = "Testname";
            var phones = new Phone[]
            {
                new Phone{Name = searchString, Company = "Apple" , Price = 25},
                new Phone{Name="Samsung j7", Company = searchString, Price=20}
            };
            _context.Phones.AddRange(phones);
            _context.SaveChanges();

            var result = await _phonesController.Index(searchString) as ViewResult;
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Phone>>(viewResult.ViewData.Model);
            Assert.True(model.Where(s => s.Name == searchString || s.Company == searchString).Count() >= 2);

        }
        [Fact]
        public async Task TestCreate()
        {


            var options = new DbContextOptionsBuilder<MobileContext>()
                              .UseInMemoryDatabase(databaseName: "phonesdb")
                              .Options;
            using (var context = new MobileContext(options))
            {
                int beforeCount = context.Phones.Count();
                var phone = new Phone
                {
                    Name = "SuperPhone",
                    Company = "KyrgyzCompany",
                    Price = 20
                };
                _phonesController = new PhonesController(context, _ihost);
                var result = await _phonesController.Create(phone);
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
                Assert.Equal(beforeCount + 1, context.Phones.Count());
            }

        }
        [Fact]
        public async Task TestDelete()
        {
            var options = new DbContextOptionsBuilder<MobileContext>()
                        .UseInMemoryDatabase(databaseName: "phonesdb")
                        .Options;
            using (var context = new MobileContext(options))
            {
                int beforeCount = context.Phones.Count();
                var phones = new Phone
                {
                  Name = "IPhone 14",
                    Company = "Apple" , 
                    Price = 20
                };
                _phonesController = new PhonesController(context, _ihost);
                var result = await _phonesController.Delete(phones);
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
                Assert.Equal(beforeCount - 1, context.Phones.Count());
            }
        }
    }
}
