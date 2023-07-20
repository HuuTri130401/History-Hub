using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.GuestAccess
{
    public class HomeModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private ITimelineRepository _timelineRepository;
        private IPeriodRepository _periodRepository;
        private IFigureRepository _figureRepository;

        public HomeModel(BusinessObjects.Models.HistoryHubContext context, ITimelineRepository timelineRepository, IPeriodRepository periodRepository, IFigureRepository figureRepository)
        {
            _context = context;
            _timelineRepository = timelineRepository;
            _periodRepository = periodRepository;
            _figureRepository = figureRepository;
        }

        public IList<Timeline> Timelines { get; set; } = default!;
        public IList<Period> Periods { get; set; } = default;
        public IList<Figure> Figures { get; set; } = default!;
        public Timeline Timeline { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_context.Timelines != null)
            {
                Timelines = await _context.Timelines
                .Include(t => t.CreatedByNavigation).ToListAsync();
            }

            if (_context.Periods != null)
            {
                Periods = await _context.Periods
                .Include(t => t.CreateByNavigation).ToListAsync();
            }

            if (_context.Figures != null)
            {
                Figures = await _context.Figures
                .Include(t => t.CreateByNavigation).ToListAsync();
            }

            if (_timelineRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }
            if (id == 0)
            {
                id = 1;
            }

            Timeline = await _context.Timelines
                .AsNoTracking()
                .Include(u => u.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.TimelineId == id);

            if (Timeline == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
