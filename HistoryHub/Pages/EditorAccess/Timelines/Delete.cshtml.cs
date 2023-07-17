using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.EditorAccess.Timelines
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public DeleteModel(BusinessObjects.Models.HistoryHubContext context)
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
            else 
            {
                Timeline = timeline;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Timelines == null)
            {
                return NotFound();
            }
            var timeline = await _context.Timelines.FindAsync(id);

            if (timeline != null)
            {
                Timeline = timeline;
                _context.Timelines.Remove(Timeline);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
