using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.EditorAccess.Periods
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IPeriodRepository _periodRepository;

        public DeleteModel(BusinessObjects.Models.HistoryHubContext context, IPeriodRepository periodRepository)
        {
            _context = context;
            _periodRepository = periodRepository;
        }

        [BindProperty]
        public Period Period { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Periods == null)
            {
                return NotFound();
            }

            Period = await _context.Periods
                .AsNoTracking()
                .Include(u => u.CreateByNavigation)
                .FirstOrDefaultAsync(m => m.PeriodId == id);

            if (Period == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool status)
        {
            if (id == null || _context.Periods == null)
            {
                return NotFound();
            }
            var period = await _periodRepository.UpdatePeriodStatus(id, status);

            if (period)
            {
                return RedirectToPage("./EditorManagePeriods");
            }
            else
            {
                return NotFound();
            }

        }
    }
}
