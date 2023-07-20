using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.EditorAccess.Figures
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public IndexModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Figure> Figure { get;set; } = default!;

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
