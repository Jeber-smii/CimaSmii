using Gestion_de_films.Models;
using Gestion_de_films.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.IO;



namespace Gestion_de_films.Controllers
{
	[Authorize(Roles = "Admin")]
	public class MovieController : Controller
    {
        readonly IMovieRepository<Movie> movieRepository;
        readonly IRoomRepository<Room> roomRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        readonly MovieContext context;
        public MovieController(IMovieRepository<Movie> movieRepository, IRoomRepository<Room> roomRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.movieRepository = movieRepository;
            this.roomRepository = roomRepository;
            this.hostingEnvironment = hostingEnvironment;
        }



        // GET: FilmController
        public ActionResult Index()
        {
            var movies = movieRepository.GetAll();
            foreach (Movie m in movies)
            {
                if (m.ShowTime < DateTime.Now.AddHours(1))
                {
                    Remove(m.MovieID, m);
                }
            }
            var rooms = roomRepository.GetAll();
            var emptyOption = new SelectListItem { Value = "", Text = "" };
            var roomList = new List<SelectListItem> { emptyOption };
            roomList.AddRange(rooms.Select(room => new SelectListItem { Value = room.RoomID.ToString(), Text = room.Name }));

            ViewBag.RoomID = new SelectList(roomList, "Value", "Text");
            return View(movieRepository.GetAll());
        }

        // GET: FilmController/Details/5
        public ActionResult Details(int id)
        {
            var movie = movieRepository.GetById(id);
            return View(movie);
        }

        // GET: FilmController/Create
        public ActionResult Create()
        {
            ViewBag.RoomID = new SelectList(roomRepository.GetAll(), "RoomID", "Name");
            return View();
        }

        // POST: FilmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie ,IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return RedirectToAction("home");
            }

            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            int nb= roomRepository.GetById(movie.RoomID).NbPlaces;
            movie.Poster = uniqueFileName;
            movie.NbPlacesDispo = nb;
            movieRepository.Add(movie);

            return RedirectToAction("Index");
        }



    
        // GET: FilmController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.RoomID = new SelectList(roomRepository.GetAll(), "RoomID", "Name");
            Movie m = movieRepository.GetById(id);
            return View(m);
        }

        // POST: FilmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Movie movie, IFormFile image)
        {
            
                Movie m = movieRepository.GetById(id);
                if (image == null || image.Length == 0)
                {
                    movie.Poster=m.Poster;

                }else {

                    string oldPosterPath = Path.Combine(hostingEnvironment.WebRootPath, "images", m.Poster);
                    if (System.IO.File.Exists(oldPosterPath))
                    {
                        System.IO.File.Delete(oldPosterPath);
                    }
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    movie.Poster = uniqueFileName;
                }
                movieRepository.Edit(movie);
                return RedirectToAction(nameof(Index));
           
        }

        // GET: FilmController/Delete/5
        public ActionResult Delete(int id)
        {
            var m = movieRepository.GetById(id);
            return View(m);
        }

        // POST: FilmController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                Remove(id, movie);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public void Remove(int id , Movie movie)
        {
            var m = movieRepository.GetById(id);
            string oldPosterPath = Path.Combine(hostingEnvironment.WebRootPath, "images", m.Poster);
            if (System.IO.File.Exists(oldPosterPath))
            {
                System.IO.File.Delete(oldPosterPath);
            }
            movieRepository.Delete(movie);
        }
        public ActionResult Search(string title, int? roomid)
        {
            var result = movieRepository.GetAll();
            if (!string.IsNullOrEmpty(title))
                result = movieRepository.FindByTitle(title);
            else
            if (roomid != null)
                result = movieRepository.GetMovieByRoomID(roomid);
            ViewBag.RoomID = new SelectList(roomRepository.GetAll(), "RoomID", "Name");
            return View("Index", result);
        }
    }
}
