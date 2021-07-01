using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trip;
using System;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public TripsController(IValidator validator, ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripViewModel model)
        {
            var modelErrors = this.validator.ValidateTripCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var parsedDate = DateTime.Parse(model.DepartureTime);

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = parsedDate,
                Seats = model.Seats,
                Description = model.Description,
                ImagePath = model.ImagePath
            };

            this.data.Trips.Add(trip);

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var allTrips = this.data.Trips
                .Include(t => t.UserTrips)
                .Select(trip => new AllTripsViewModel
                {
                    Id = trip.Id,
                    StartPoint = trip.StartPoint,
                    EndPoint = trip.EndPoint,
                    Seats = trip.Seats - trip.UserTrips.Count,
                    DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm")
                })
                .ToList();

            return View(allTrips);
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data.Trips
                .Include(t => t.UserTrips)
                .FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                return Error($"Trip does not exist. ID: '{tripId}'");
            }

            var tripViewModel = new TripDetailsViewModel
            {
                Id = trip.Id,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = trip.Seats - trip.UserTrips.Count,
                Description = trip.Description,
                ImagePath = trip.ImagePath
            };

            return View(tripViewModel);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var trip = this.data.Trips
                .Include(t => t.UserTrips)
                .FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                return Error($"Trip does not exist. ID: '{tripId}'");
            }

            var userIsAlreadyIn = this.data.UserTrips
                .FirstOrDefault(ut => ut.UserId == this.User.Id && ut.TripId == tripId) != null;
            
            // user is already in the trip
            if (userIsAlreadyIn)
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            // if there are no more free seats
            if (trip.UserTrips.Count >= trip.Seats)
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            var userTrip = new UserTrip
            {
                UserId = this.User.Id,
                TripId = tripId
            };

            this.data.UserTrips.Add(userTrip);

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}
