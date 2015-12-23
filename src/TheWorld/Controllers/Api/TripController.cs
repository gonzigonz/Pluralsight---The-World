using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
	[Authorize]
	[Route("api/trips")]
	public class TripController : Controller
    {
		private ILogger<TripController> _logger;
		private IWorldRepository _worldRepo;

		public TripController(IWorldRepository worldRepo, ILogger<TripController> logger)
		{
			_worldRepo = worldRepo;
			_logger = logger;
		}

        [HttpGet("")]
        public JsonResult Get()
        {
			var trips = _worldRepo.GetUserTripsWithStops(User.Identity.Name);
			var result = Mapper.Map<IEnumerable<TripViewModel>>(trips);
			return Json(result);
        }

		[HttpPost("")]
		public JsonResult Post([FromBody]TripViewModel vm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var newTrip = Mapper.Map<Trip>(vm);
					newTrip.UserName = User.Identity.Name;

					// Save to the Database
					_logger.LogInformation("Attempting to save a new trip.");
					_worldRepo.AddTrip(newTrip);

					if (_worldRepo.SaveAll())
					{
						Response.StatusCode = (int)HttpStatusCode.Created;
						return Json(Mapper.Map<TripViewModel>(newTrip));
					}
				}
			}
			catch (Exception e)
			{
				_logger.LogError("Failed to save new trip", e);
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json(new { Message = e.Message});
			}
			

			Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return Json(new { Message = "Failed", ModelState = ModelState });
;		}
    }
}
