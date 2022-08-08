using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
		public AccountController(IAccountService accountService, IUserRepository userRepository, IConfiguration configuration)
		{
			_accountService = accountService;
			_userRepository = userRepository;
			_configuration = configuration;
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
            var user = await _accountService.ValidateUser(model);
            if (user != null)
            {
                //create token
                var jwtToken = CreateJWTToken(user);
                return Ok(new { token = jwtToken });
            }
            //client will send info to email/pass to api, post
            //api will create the JWT token and send it to client
            //client will store token somewhere
            //angular, react (local storage or sesssion storage)
            //ios/android (device)
            //next time when client need secure info or needs to perform any operation that requires authentication.
            //client will send the token to the API in the HTTP Header
            //once api recives the token from client, it will validate the JWT token and if validated it will send the data back to the client
            //if JWT token is invalid or token is expired then api will send 401
            throw new UnauthorizedAccessException("Please cgeck email and password");
            //return Unauthorized(new { errorMessage = "Please check email and password" });
        }
        private string CreateJWTToken(UserInfoResponseModel user) 
        {
            //create token
            //create the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("Country", "USA"),
                new Claim("Language", "English")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            //specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            //specify the algo
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //specify the expiration
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            //create an object with all the above information to create token
            var tokendetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials =credential,
                Issuer = "MovieShop Inc",
                Audience =  "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokendetails);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
