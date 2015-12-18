using Microsoft.AspNet.Mvc;
using TheWorld.Models;

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
		public JsonResult Post([FromBody]Trip newTrip)
		{
			return Json(true)
;		}
    }
}
