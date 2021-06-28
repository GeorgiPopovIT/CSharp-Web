using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services;
using CarShop.ViewModels.Cars;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext context;
        public CarsController(IValidator validator, ApplicationDbContext context)
        {
            this.context = context;
            this.validator = validator;
        }
        [Authorize]
        public HttpResponse All()
        {
            //return View();
            var cars = this.context.Cars
                .AsQueryable();

            var user = this.context.Users.FirstOrDefault(u => u.Id == this.User.Id);
            if (user.IsMechanic)
            {
                cars = cars.Where(c => c.Issues.Any(c => !c.IsFixed));
            }
            else
            {
                cars = cars.Where(c => c.OwnerId == this.User.Id);
            }

            var carsToList = cars.Select(c => new ListingCarsModel
            {
                Id = c.Id,
                Model = c.Model,
                ImgUrl = c.PictureUrl,
                Year = c.Year,
                Plate = c.PlateNumber,
                FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count()
            }).ToList();

            return View(carsToList);
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CreateCarModel model)
        {
            var errorsModel = this.validator.ValidateCar(model);
            if (this.context.Cars.Any(c => c.Model == model.Model 
            && c.PlateNumber == model.PlateNumber))
            {
                errorsModel.Add("This car is already in added.");
            }
            if (errorsModel.Any())
            {
                return Error(errorsModel);
            }

            var car = new Car
            {
                Model = model.Model,
                PictureUrl = model.ImageUrl,
                PlateNumber = model.PlateNumber,
                Year = model.Year,
                OwnerId = this.User.Id
            };

            this.context.Cars.Add(car);

            this.context.SaveChanges();

            return Redirect("/Cars/All");
        }
    }
}
