using Microsoft.AspNet.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
	public class TripController : Controller
    {
		private IWorldRepository _worldRepo;

		public TripController(IWorldRepository worldRepo)
		{
			_worldRepo = worldRepo;
		}

        [HttpGet("api/trips")]
        public JsonResult Get()
        {
			var result = _worldRepo.GetAllTripsWithStops();
			return Json(result);
        }
    }
}
