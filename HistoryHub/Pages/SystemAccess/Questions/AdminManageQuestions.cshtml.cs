using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.Questions
{
    public class AdminManageQuestionsModel : PageModel
    {
        private readonly HistoryHubContext _context;

        public AdminManageQuestionsModel(HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Question> Question{ get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Quizzes != null)
            {
                Question = await _context.Questions
                .Include(q => q.CreatedByNavigation)
                .Include(q => q.Quiz)
                .OrderBy(qu => qu.Question1)
                .OrderBy(qu => qu.QuizId)
                .ToListAsync();
            }
        }
    }
}
