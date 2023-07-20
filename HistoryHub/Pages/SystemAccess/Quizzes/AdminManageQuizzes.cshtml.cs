using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> OnGetAsync()
        {
            //if (_context.Quizzes != null)
            //{
            //    Quiz = await _context.Quizzes
            //    .Include(q => q.CreatedByNavigation)
            //    .Include(q => q.Timeline).ToListAsync();
            //}
            int? roleId = HttpContext.Session.GetInt32("HasRole");

            if (roleId != null)
            {
                if (roleId == 1 || roleId == 2)
                {
                    if (_context.Quizzes != null)
                    {
                        Quiz = await _context.Quizzes
                        .Include(q => q.CreatedByNavigation)
                        .Include(q => q.Timeline).ToListAsync();
                    }
                }
                if (roleId == 3)
                {
                    TempData["ErrorMessage"] = "You not have permission";
                    return RedirectToPage("/Account/Login");
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
