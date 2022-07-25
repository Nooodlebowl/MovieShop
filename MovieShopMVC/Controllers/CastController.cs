using Microsoft.AspNetCore.Mvc;
using ApplicationCore.ServiesContracts;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);
            return View(castDetails);
        }
    }
}
