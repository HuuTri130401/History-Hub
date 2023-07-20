using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Timelines
{
    public class DeleteModel : PageModel
    {
        private readonly HistoryHubContext _context;
        private readonly ITimelineRepository _timelineRepository;

        public DeleteModel(HistoryHubContext context, ITimelineRepository timelineRepository)
        {
            _context = context;
            _timelineRepository = timelineRepository;
        }

        [BindProperty]
        public Timeline Timeline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Timelines == null)
            {
                return NotFound();
            }


            Timeline = await _context.Timelines.AsNoTracking()
                .Include(u => u.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.TimelineId == id);

            if (Timeline == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool status)
        {
            if (id == null || _context.Timelines == null)
            {
                return NotFound();
            }
            var timeline = await _timelineRepository.UpdateTimlineStatus(id, status);

            if (timeline)
            {
                return RedirectToPage("./AdminManageTimelines");
            }
            else
            {
                return NotFound();
            }


        }
    }
}
