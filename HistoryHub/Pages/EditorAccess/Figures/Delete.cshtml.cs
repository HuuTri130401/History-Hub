using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.EditorAccess.Figures
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IFigureRepository _figureRepository;

        public DeleteModel(BusinessObjects.Models.HistoryHubContext context, IFigureRepository figureRepository)
        {
            _context = context;
            _figureRepository = figureRepository;
        }

        [BindProperty]
        public Figure Figure { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Figures == null)
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

        public async Task<IActionResult> OnPostAsync(int? id, bool status)
        {
            if (id == null || _context.Figures == null)
            {
                return NotFound();
            }
            var figure = await _figureRepository.UpdateFigureStatus(id, status);

            if (figure)
            {
                return RedirectToPage("./EditorManageFigures");
            }
            else
            {
                return NotFound();
            }


        }
    }
}
