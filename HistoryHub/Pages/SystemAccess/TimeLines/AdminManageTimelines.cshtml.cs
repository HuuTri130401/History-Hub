using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.TimeLines
{
    public class AdminManageTimelinesModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public AdminManageTimelinesModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Timeline> Timeline { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Timelines != null)
            {
                Timeline = await _context.Timelines
                .Include(t => t.CreatedByNavigation).ToListAsync();
            }
        }
    }
}
