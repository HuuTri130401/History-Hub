using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.Figures
{
    public class AdminManageFiguresModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public AdminManageFiguresModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Figure> Figure { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Figures != null)
            {
                Figure = await _context.Figures
                .Include(f => f.CreateByNavigation)
                .Include(f => f.Period).ToListAsync();
            }
        }
    }
}
