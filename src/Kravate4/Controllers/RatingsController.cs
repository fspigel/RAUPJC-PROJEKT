using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kravate4.Data;
using Kravate4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Kravate4.Controllers
{
    public class RatingsController : Controller
    {
        private readonly KravateDbContext _context;
        private readonly UserManager<ApplicationUser> _uManager;

        public RatingsController(KravateDbContext context, UserManager<ApplicationUser> uManager)
        {
            _context = context;
            _uManager = uManager;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var kravateDbContext = _context.Rating.Include(r => r.Politician);
            return View(await kravateDbContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PoliticianID,Value")] Rating rating)
        {
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", rating.PoliticianID);
            var politician = _context.Politician.FirstOrDefault(s => s.ID == rating.PoliticianID);
            if (ModelState.IsValid)
            {

                rating.ID = Guid.NewGuid();
                rating.UserID = await GetCurrentUser();
                rating.Politician = politician;
                var preExistingRating = await _context.Rating.FirstOrDefaultAsync(r => r.PoliticianID == rating.PoliticianID && r.UserID == rating.UserID);
                if (preExistingRating == default(Rating))
                {
                    politician.Ratings.Add(rating);
                    _context.Update(politician);
                    //_context.Update(rating.Politician);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    preExistingRating.Value = rating.Value;
                    preExistingRating.DateUpdated = DateTime.Now;
                    _context.Update(politician);
                    await _context.SaveChangesAsync();
                }   
                return View(politician);
            }
            return View(rating);
        }

        [Authorize]
        public async Task<IActionResult> Rate(Guid politicianID, short value)
        {
            Rating r = new Rating(value);
            r.PoliticianID = politicianID;
            await Create(r);
            return Redirect("~/politicians/Details/" + r.Politician.ID);
        }
        
        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", rating.PoliticianID);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,DateCreated,DateUpdated,PoliticianID,UserID,Value")] Rating rating)
        {
            if (id != rating.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", rating.PoliticianID);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

            //return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<Guid> GetCurrentUser()
        {
            var user = await _uManager.GetUserAsync(HttpContext.User);
            return new Guid(user.Id);
        }

        private bool RatingExists(Guid id)
        {
            return _context.Rating.Any(e => e.ID == id);
        }
    }
}
