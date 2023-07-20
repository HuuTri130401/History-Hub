using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IList<Question> Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? roleId = HttpContext.Session.GetInt32("HasRole");

            if (roleId != null)
            {
                if (roleId == 1 || roleId == 2)
                {
                    if (_context.Questions != null)
                    {
                        Question = await _context.Questions
                        .Include(q => q.CreatedByNavigation)
                        .Include(q => q.Quiz)
                        .OrderBy(qu => qu.Question1)
                        .OrderBy(qu => qu.QuizId)
                        .ToListAsync();
                    }
                }
                if (roleId == 3)
                {
                    TempData["ErrorMessage"] = "You not have permission";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Can not access this page, please Login";
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
    }
}
