using Gestion_de_films.Models;
using Gestion_de_films.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_de_films.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoomController : Controller
    {
        readonly IRoomRepository<Room> roomRepository;

        public RoomController(IRoomRepository<Room> roomRepository)
        {
            this.roomRepository = roomRepository;
        }


        // GET: SalleCinemaController
        public ActionResult Index()
        {
            var salles = roomRepository.GetAll();
            return View(salles);
        }

        // GET: SalleCinemaController/Details/5
        public ActionResult Details(int id)
        {
            var salle = roomRepository.GetById(id);
            return View(salle);
        }

        // GET: SalleCinemaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalleCinemaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            try
            {
                roomRepository.add(room);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalleCinemaController/Edit/5
        public ActionResult Edit(int id)
        {
            var salle = roomRepository.GetById(id);
            return View(salle);
        }

        // POST: SalleCinemaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Room room)
        {
            try
            {
                roomRepository.Edit(room);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalleCinemaController/Delete/5
        public ActionResult Delete(int id)
        {
            var salle = roomRepository.GetById(id);
            return View(salle);
        }

        // POST: SalleCinemaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Room room)
        {
            try
            {
                roomRepository.Delete(room);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
