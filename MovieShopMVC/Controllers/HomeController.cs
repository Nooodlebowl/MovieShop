using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //action methods inside the controller
        //page opens up (routing) to "index" by default because of program.cs
        [HttpGet]
        //good practice to denote the type of http request
        public IActionResult Index()
        {
            //it only returns view but if you open the view folder there is 2 views
            //how does it know to open the index one?

            //passing data from controller/action methods to views, through c# models
            var movieService = new MovieService();
            var movieCards = movieService.GetTopRevenueMovies();
            return View(movieCards);
            //going to pick a view with the same method name
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TopRatedMovies() 
        {
            //will error if there is no corrisponding html file
            //Will look for TopRatedMovies.cshtml
            return View();
            //retrun View("Privacy"); // can specify the view, doesn't have to match the method name
        
            //Home Page
            //Top 30 highest grossing movies
            //go to database and get 30 movies
            //List<Movie> 
            //we need to send the model data to the view

            //controllers will call servies which are going to call repositories
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}