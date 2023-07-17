using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.Quizzes
{
    public class AdminManageQuizzesModel : PageModel
    {
        private readonly HistoryHubContext _context;

        public AdminManageQuizzesModel(HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Quiz> Quiz { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Quizzes != null)
            {
                Quiz = await _context.Quizzes
                .Include(q => q.CreatedByNavigation)
                .Include(q => q.Timeline).ToListAsync();
            }
        }
    }
}
