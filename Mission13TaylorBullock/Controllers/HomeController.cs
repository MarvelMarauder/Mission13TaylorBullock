using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13TaylorBullock.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13TaylorBullock.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository repo { get; set; }

        public HomeController(IBowlersRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string team)
        {
            object bowlers = null;
            ViewBag.Id = null;

            if (team == null)
            {
                bowlers = repo.Bowlers
                    .Include(x => x.Team)
                    .OrderBy(x => x.BowlerLastName)
                    .ToList();
            }
            else
            {
                bowlers = repo.Bowlers
                    .Where(x => x.Team.TeamName == team)
                    .OrderBy(x => x.BowlerLastName)
                    .Include(x => x.Team)
                    .ToList();
            }

            
            
            return View(bowlers);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = repo.Teams.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            if (ModelState.IsValid)
            {
                var i = repo.Bowlers.Count();
                b.BowlerID = i + 1;
                repo.CreateBowler(b);
                return View("Confirmation", b);
            }
            else //if invalid, send back to the form and see error messages
            {
                ViewBag.Teams = repo.Teams.ToList();
                return View(b);
            }
        }

        [HttpGet]
        public IActionResult EditBowler(int bowlerid)
        {
            ViewBag.Teams = repo.Teams.ToList();

            var stuff = repo.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View("AddBowler", stuff);
        }

        [HttpPost]
        public IActionResult EditBowler(Bowler b)
        {
            repo.SaveBowler(b);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteBowler(int bowlerid)
        {
            var to_delete = repo.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View(to_delete);
        }

        [HttpPost]
        public IActionResult DeleteBowler(Bowler b)
        {

            repo.DeleteBowler(b);

            return RedirectToAction("Index");
        }


    }
}
