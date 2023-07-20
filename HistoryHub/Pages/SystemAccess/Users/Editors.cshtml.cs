using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HistoryHub.Pages.SystemAccess.Users
{
    public class EditorsModel : PageModel
    {
        private readonly HistoryHubContext _context;

        public EditorsModel(HistoryHubContext context)
        {
            _context = context;
        }
        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.Role)
                .Where(r => r.RoleId == 2)
                .ToListAsync();
            }
        }
    }
}
