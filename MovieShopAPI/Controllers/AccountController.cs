using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;

		public AccountController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = _accountService.CreateUser(model);
            return Ok(user);
        }
        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> checkEmail(string email) 
        {
            var check = _userRepository.GetUserByEmail(email);
            return Ok(check);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = _accountService.ValidateUser(model);
            return Ok(user);
        }
    }
}
