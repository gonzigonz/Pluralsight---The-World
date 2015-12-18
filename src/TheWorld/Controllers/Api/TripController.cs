using AutoMapper;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
	[Route("api/trips")]
	public class TripController : Controller
    {
		private IWorldRepository _worldRepo;

		public TripController(IWorldRepository worldRepo)
		{
			_worldRepo = worldRepo;
		}

        [HttpGet("")]
        public JsonResult Get()
        {
			var result = Mapper.Map<IEnumerable<TripViewModel>>(_worldRepo.GetAllTripsWithStops());
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

					// Save to the Database

					Response.StatusCode = (int)HttpStatusCode.Created;
					return Json(Mapper.Map<TripViewModel>(newTrip));
				}
			}
			catch (Exception e)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json(new { Message = e.Message});
			}
			

			Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return Json(new { Message = "Failed", ModelState = ModelState });
;		}
    }
}
