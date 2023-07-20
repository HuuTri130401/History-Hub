using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.Figures
{
    public class EditModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public EditModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Figure Figure { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Figures == null)
            {
                return NotFound();
            }

            var figure = await _context.Figures.FirstOrDefaultAsync(m => m.FigureId == id);
            if (figure == null)
            {
                return NotFound();
            }
            Figure = figure;
            ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["PeriodId"] = new SelectList(_context.Periods, "PeriodId", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Figure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FigureExists(Figure.FigureId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./AdminManageFigures");
        }

        private bool FigureExists(int id)
        {
            return (_context.Figures?.Any(e => e.FigureId == id)).GetValueOrDefault();
        }
    }
}
