using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

		public CastController(ICastService castService)
		{
			_castService = castService;
		}
        [HttpGet]
        [Route("Cast/{id}")]
        public async Task<IActionResult> getCastDetails(int castId) 
        {
            var castDetails = await _castService.GetCastDetails(castId);
            return Ok(castDetails);
        }
	}
}
