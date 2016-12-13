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
            return View(db.Events.Include(e => e.Genre).Where(e => (e.Artist.UserName == User.Identity.Name && !e.IsCancelled)).ToList());
        }
        public IActionResult UserIndex(string search)
        {
            var events = db.Events.Include(e => e.Genre).Include(e => e.Artist).Include(e => e.Users).Where(e => e.Date > DateTime.Today && !e.IsCancelled).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                events = events.Where(e => e.Venue.ToUpper().Contains(search.ToUpper()) || e.Artist.Name.ToUpper().Contains(search.ToUpper()) || e.Genre.Name.ToUpper().Contains(search.ToUpper())).ToList();
            }
            return View(events);
        }
        public IActionResult UserEvents()
        {
            return View(db.Events.Include(e => e.Genre).Include(e => e.Artist).Where(e => e.Users.Any(u => u.User.UserName == User.Identity.Name)).ToList());
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
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            if (id == null)
            {
                return NotFound();
            }
            return View(db.Events.SingleOrDefault(e => e.EventID == id));
        }
        [HttpPost]
        public IActionResult Edit(Event show)
        {
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            if (show == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                db.Update(show);
                db.SaveChanges();
                return RedirectToAction("ArtistIndex");
            }
            return View(db.Events.SingleOrDefault(e => e.EventID == show.EventID));
        }
        public IActionResult Cancel(bool confirm, int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            if (confirm)
            {
                db.Events.SingleOrDefault(e => e.EventID == id).IsCancelled = true;
                db.SaveChanges();
                return RedirectToAction("ArtistIndex");
            }
            else return RedirectToAction("ArtistIndex");
        }
        public IActionResult Reserve(int id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            db.Attendance.Add(new Attendance { UserID = user.Id, EventID = id });
            db.SaveChanges();
            return RedirectToAction("UserIndex");
        }
        public IActionResult Unattend(int id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            db.Remove(db.Attendance.SingleOrDefault(e => e.UserID == user.Id && e.EventID == id));
            db.SaveChanges();
            return RedirectToAction("UserIndex");
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(db.Events.Include(e => e.Artist).Include(e => e.Genre).SingleOrDefault(e => e.EventID == id));
        }
        public IActionResult Follow(string id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            db.Following.Add(new Following {FollowerID = user.Id, ArtistID = id});
            db.SaveChanges();
            return RedirectToAction("UserIndex");
        }
        public IActionResult UnFollow(string id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            db.Remove(db.Following.SingleOrDefault(f => f.ArtistID == id && f.FollowerID == user.Id));
            db.SaveChanges();
            return RedirectToAction("UserIndex");
        }
        public IActionResult ShowFollowing()
        {
            return View(db.Following.Include(f => f.Artist).Where(f => f.Follower.UserName == User.Identity.Name).ToList());
        }
    }
}
