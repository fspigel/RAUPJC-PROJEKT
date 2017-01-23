using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kravate4.Data;
using Kravate4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Kravate4.Controllers
{
    public class CommentsController : Controller
    {
        private readonly KravateDbContext _context;
        private readonly UserManager<ApplicationUser> _uManager;

        public CommentsController(KravateDbContext context, UserManager<ApplicationUser> uManager)
        {
            _context = context;
            _uManager = uManager;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var kravateDbContext = _context.Comment.Include(c => c.Politician);
            return View(await kravateDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.SingleOrDefaultAsync(m => m.ID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Body,PoliticianID")] Comment comment)
        {
            var politician = _context.Politician.FirstOrDefault(p => p.ID == comment.PoliticianID);
            if (ModelState.IsValid)
            {
                comment.ID = Guid.NewGuid();
                comment.Politician = politician;
                politician.Comments.Add(comment);
                _context.Add(comment);
                _context.Update(politician);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", comment.PoliticianID);
            return View(comment);
        }

        [Authorize]
        public async Task<IActionResult> LeaveComment(Guid politicianID, string text)
        {
            Comment c = new Comment();
            c.PoliticianID = politicianID;
            c.Body = text;
            await Create(c);
            return Redirect("~/politician/Details" + c.PoliticianID);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.SingleOrDefaultAsync(m => m.ID == id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", comment.PoliticianID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Body,DateCreated,DateUpdated,PoliticianID,UserID")] Comment comment)
        {
            if (id != comment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.ID))
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
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", comment.PoliticianID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.SingleOrDefaultAsync(m => m.ID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comment = await _context.Comment.SingleOrDefaultAsync(m => m.ID == id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comment.Any(e => e.ID == id);
        }
    }
}
