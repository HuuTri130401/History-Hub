using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.EditorAccess.Timelines
{
    public class EditorManageTimelinesModel : PageModel
    {

        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public EditorManageTimelinesModel(BusinessObjects.Models.HistoryHubContext context)
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
