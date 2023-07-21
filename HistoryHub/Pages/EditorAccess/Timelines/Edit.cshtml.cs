using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.EditorAccess.Timelines
{
    public class EditModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public EditModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Timeline Timeline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Timelines == null)
            {
                return NotFound();
            }

            var timeline = await _context.Timelines.FirstOrDefaultAsync(m => m.TimelineId == id);
            if (timeline == null)
            {
                return NotFound();
            }
            Timeline = timeline;
            ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "Email");
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

            _context.Attach(Timeline).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimelineExists(Timeline.TimelineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./EditorManageTimelines");
        }

        private bool TimelineExists(int id)
        {
            return (_context.Timelines?.Any(e => e.TimelineId == id)).GetValueOrDefault();
        }
    }
}
