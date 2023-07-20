using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.EditorAccess.Periods
{
    public class EditorManagePeriodsModel : PageModel
    {

        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public EditorManagePeriodsModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Period> Period { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Periods != null)
            {
                Period = await _context.Periods
                .Include(p => p.CreateByNavigation)
                .Include(p => p.Timeline).ToListAsync();
            }
        }

    }
}
