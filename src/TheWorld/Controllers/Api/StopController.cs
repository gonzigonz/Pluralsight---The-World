using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
	[Authorize]
	[Route("api/trips/{tripName}/stops")]
	public class StopController : Controller
    {
		private CoordService _coordService;
		private ILogger<StopController> _logger;
		private IWorldRepository _worldRepo;

		public StopController(IWorldRepository worldRepo, ILogger<StopController> logger,
			CoordService coordService)
		{
			_worldRepo = worldRepo;
			_logger = logger;
			_coordService = coordService;
		}

		[HttpGet("")]
		public JsonResult Get(string tripName)
		{
			try
			{
				var results = _worldRepo.GetTripsByName(tripName, User.Identity.Name);
				if (results == null)
				{
					return Json(null);
				}
				return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops
					.OrderBy(s => s.Order)
					));
			}
			catch (Exception e)
			{
				_logger.LogError($"Failed to get stops for trip '{tripName}'", e);
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("Error occured finding trip name.");
			}
		}

		[HttpPost("")]
		public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vm)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Map to the Entity
					var newStop = Mapper.Map<Stop>(vm);

					// Looking up Geocoordinates
					var coordResult = await _coordService.Lookup(newStop.Name);
					if (!coordResult.Success)
					{
						_logger.LogError(coordResult.Message);
						Response.StatusCode = (int)HttpStatusCode.BadRequest;
						return Json(coordResult.Message);
					}

					newStop.Latitude = coordResult.Latitude;
					newStop.Longitude = coordResult.Longitude;

					// Save to the Database
					_worldRepo.AddStop(tripName, User.Identity.Name, newStop);
					if (_worldRepo.SaveAll())
					{
						Response.StatusCode = (int)HttpStatusCode.Created;
						return Json(Mapper.Map<StopViewModel>(newStop));
					}
				}
				return Json(true);
			}
			catch (Exception e)
			{
				_logger.LogError("Failed to save new stop", e);
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json("Failed to save new stop");
			}
		}
    }
}
