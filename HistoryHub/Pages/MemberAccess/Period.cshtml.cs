using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.MemberAccess
{
    public class PeriodModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private IFigureRepository _figureRepository;
        private IPeriodRepository _periodRepository;

        public PeriodModel(BusinessObjects.Models.HistoryHubContext context, IFigureRepository figureRepository, IPeriodRepository periodRepository)
        {
            _context = context;
            _figureRepository = figureRepository;
            _periodRepository = periodRepository;
        }
        public IList<Timeline> Timelines { get; set; } = default!;
        public IList<Period> Periods { get; set; } = default!;
        public Period Period { get; set; } = default;
        public IList<Figure> Figures { get; set; } = default!;

        public IList<Figure> AllFigure { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_context.Timelines != null)
            {
                Timelines = await _context.Timelines
                .Include(t => t.CreatedByNavigation).Where(s => s.Status == true).ToListAsync();
            }

            if (_context.Periods != null)
            {
                Periods = await _context.Periods
                .Include(t => t.CreateByNavigation).Where(s => s.Status == true).ToListAsync();

            }

            if (_context.Figures != null)
            {
                AllFigure = await _context.Figures
                .Include(t => t.CreateByNavigation).Where(s => s.Status == true).ToListAsync();
            }


            if (_context.Figures != null)
            {
                Figures = await _context.Figures
                .Where(p => p.PeriodId == id).ToListAsync();

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
    }
}
