using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services;
using SharedTrip.ViewModels;
using SharedTrip.ViewModels.Trips;
using System;
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
                .Select(t => new ListingAllTripsModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats - t.Users.Count

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
                DepartureTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
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
                    .FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                return Error("Invalid trip.");
            }

            return View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var userId = this.User.Id;

            var currTrip = this.context.Trips.FirstOrDefault(t => t.Id == tripId);

            //if (currTrip.Id == tripId && currTrip.Users.Any(u => u.Id == userId))
            //{
            //    return Redirect($"/Trips/Details?tripId={tripId}");
            //}
            if (currTrip.Seats - currTrip.Users.Count <= 0)
            {
                return Error("No seats avaible.");
            }

            var currUser = this.context.Users.FirstOrDefault(u => u.Id == userId);


            try
            {
                currUser.Trips.Add(currTrip);
                this.context.SaveChanges();
            }
            catch (Exception)
            {

                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            return Redirect("/Trips/All");
        }
    }
}
