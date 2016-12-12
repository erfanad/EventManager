using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManager.Data;

namespace EventManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index(string search)
        {
            var events = db.Events.Include(e => e.Genre).Include(e => e.Artist).Include(e => e.Users).Where(e => e.Date > DateTime.Now).ToList();
            ViewBag.Attendance = db.Attendance.Include(e => e.Event).Where(e => e.User.UserName == User.Identity.Name).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                events = db.Events.Where(e => e.Artist.Name.Contains(search) || e.Venue.Contains(search) || e.Genre.Name.Contains(search)).ToList();
            }
            return View(events);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
