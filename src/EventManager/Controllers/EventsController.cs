using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventManager.Data;
using Microsoft.AspNetCore.Identity;
using EventManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManager.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
           db = context;
            _userManager = userManager;
        }
        public IActionResult ArtistIndex()
        {
            ViewBag.Artist = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            return View(db.Events.Include(e => e.Genre).Where(e => e.Artist.UserName == User.Identity.Name).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Event show)
        {
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            show.Artist = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            if (ModelState.IsValid)
            {
                db.Add(show);
                db.SaveChanges();
                return RedirectToAction("ArtistIndex");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(db.Events.SingleOrDefault(e => e.EventID == id));
        }
    }
}
