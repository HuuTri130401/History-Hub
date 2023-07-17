using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.EditorAccess.Periods
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public DeleteModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Period Period { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Periods == null)
            {
                return NotFound();
            }

            var period = await _context.Periods.FirstOrDefaultAsync(m => m.PeriodId == id);

            if (period == null)
            {
                return NotFound();
            }
            else 
            {
                Period = period;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Periods == null)
            {
                return NotFound();
            }
            var period = await _context.Periods.FindAsync(id);

            if (period != null)
            {
                Period = period;
                _context.Periods.Remove(Period);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
