using Gestion_de_films.Models;
using Gestion_de_films.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace Gestion_de_films.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IMovieRepository<Movie> movieRepository;
        readonly IRoomRepository<Room> roomRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IMovieRepository<Movie> movieRepository, IWebHostEnvironment hostingEnvironment, IRoomRepository<Room> roomRepository)
        {
            _logger = logger;
            this.movieRepository = movieRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.roomRepository = roomRepository;
        }

        public IActionResult Index()
        {
            var movies = movieRepository.GetAll();
            foreach (Movie m in movies)
            {
                if (m.ShowTime < DateTime.Now.AddHours(1))
                {
                    Remove(m.MovieID, m);
                }
            }
            return View(movieRepository.GetAll());
        }
        public IActionResult Details(int id)
        {
            var film = movieRepository.GetById(id);
            return View(film);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void Remove(int id, Movie movie)
        {
            var m = movieRepository.GetById(id);
            string oldPosterPath = Path.Combine(hostingEnvironment.WebRootPath, "images", m.Poster);
            if (System.IO.File.Exists(oldPosterPath))
            {
                System.IO.File.Delete(oldPosterPath);
            }
            movieRepository.Delete(movie);
        }

        public ActionResult Search(string title)
        {
            var result = movieRepository.GetAll();
            if (!string.IsNullOrEmpty(title))
                result = movieRepository.FindByTitle(title);
            return View("Index", result);
        }
    }
}
