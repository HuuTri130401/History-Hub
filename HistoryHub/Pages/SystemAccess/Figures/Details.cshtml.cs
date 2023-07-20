using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Figures
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IFigureRepository _figureRepository;

        public DetailsModel(BusinessObjects.Models.HistoryHubContext context, IFigureRepository figureRepository)
        {
            _context = context;
            _figureRepository = figureRepository;
        }

        public Figure Figure { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _figureRepository.GetAll().ToList() == null)
            {
                return NotFound();
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
