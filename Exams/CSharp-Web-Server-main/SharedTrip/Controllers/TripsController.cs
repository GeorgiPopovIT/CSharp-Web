using Git.Service;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.ViewModels.Trips;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext context;
        public TripsController(IValidator validator, ApplicationDbContext context)
        {
            this.context = context;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var allTrips = this.context.Trips
                .Select(t => new ListingTripsModel
                {
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats
                    
                }).ToList();


            return View(allTrips);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripModel model)
        {
            var errorsModel = this.validator.ValidateTrip(model);
            if (this.context.Trips.Any(t => t.EndPoint == model.EndPoint
            && t.StartPoint == model.StartPoint))
            {
                errorsModel.Add("This trip is already entered.");
            }
            if (errorsModel.Any())
            {
                return Error(errorsModel);
            }

            var trip = new Trip
            {
                EndPoint = model.EndPoint,
                StartPoint = model.StartPoint,
                DepartureTime = model.DepartureTime,
                Description = model.Description,
                ImagePath = model.ImagePath,
                Seats = model.Seats
            };

            this.context.Trips.Add(trip);

            this.context.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.context.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new DetailsTripModel
                {
                    Id = t.Id,
                    EndPoint = t.EndPoint,
                    StartPoint = t.StartPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm",CultureInfo.InvariantCulture),
                    Description = t.Description,
                    ImagePath = t.ImagePath,
                    Seats = t.Seats
                })
                .FirstOrDefault();
         
            return View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip()
        {
            //var user = this.context.Users
            //    .FirstOrDefault(u => u.Id == this.User.Id);

            //if (user.Trips.Any(t => t.Id == tripId))
            //{
            //    return Redirect($"/Trips/Details?tripId={tripId}");
            //}

            return Redirect("/home");
        }
    }
}
