using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.EditorAccess.Favorites
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public IndexModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Favorite> Favorite { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Favorites != null)
            {
                Favorite = await _context.Favorites
                .Include(f => f.Quiz)
                .Include(f => f.Timeline)
                .Include(f => f.User).ToListAsync();
            }
        }
    }
}
