using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.EditorAccess.Feedbacks
{
    public class EditorManageFeedbacksModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public EditorManageFeedbacksModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Feedback> Feedback { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Feedbacks != null)
            {
                Feedback = await _context.Feedbacks
                .Include(f => f.Quiz)
                .Include(f => f.Timeline)
                .Include(f => f.User).ToListAsync();
            }
        }
    }
}
