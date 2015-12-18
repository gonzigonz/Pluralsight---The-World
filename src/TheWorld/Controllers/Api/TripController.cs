using Microsoft.AspNet.Mvc;
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
			var result = _worldRepo.GetAllTripsWithStops();
			return Json(result);
        }

		[HttpPost("")]
		public JsonResult Post([FromBody]TripViewModel newTrip)
		{
			if (ModelState.IsValid)
			{
				Response.StatusCode = (int)HttpStatusCode.Created;
				return Json(true);
			}

			Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return Json(new { Message = "Failed", ModelState = ModelState });
;		}
    }
}
