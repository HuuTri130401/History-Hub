using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.MemberAccess
{
    public class FigureModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IFigureRepository _figureRepository;

        public FigureModel(BusinessObjects.Models.HistoryHubContext context, IFigureRepository figureRepository)
        {
            _context = context;
            _figureRepository = figureRepository;
        }

        public Figure Figure { get; set; } = default;
        public IList<Timeline> Timelines { get; set; } = default!;
        public IList<Period> Periods { get; set; } = default!;
        public IList<Figure> Figures { get; set; } = default!;


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
                Figures = await _context.Figures
                .Where(p => p.PeriodId == id).Where(s => s.Status == true).ToListAsync();

            }

            Figure = await _context.Figures
                .AsNoTracking()
                .Include(u => u.CreateByNavigation)
                .FirstOrDefaultAsync(m => m.FigureId == id);

            if (Figure == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
