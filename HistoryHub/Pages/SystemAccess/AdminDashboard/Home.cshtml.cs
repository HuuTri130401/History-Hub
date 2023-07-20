using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.AdminDashboard
{
    public class HomeModel : PageModel
    {
        private readonly HistoryHubContext _context;

        public HomeModel(HistoryHubContext context)
        {
            _context = context;
        }
        public IList<User> User { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            int? roleId = HttpContext.Session.GetInt32("HasRole");

            if (roleId != null)
            {
                if (roleId == 1)
                {
                    if (_context.Users != null)
                    {
                        User = await _context.Users
                        .Include(u => u.Role)
                        .Where(r => r.RoleId == 2)
                        .ToListAsync();
                    }
                }
                if (roleId == 2 || roleId == 3)
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
