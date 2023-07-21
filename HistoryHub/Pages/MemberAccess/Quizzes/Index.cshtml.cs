using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.MemberAccess
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public IndexModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Quiz> Quiz { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Quizzes != null)
            {
                Quiz = await _context.Quizzes
                .Include(q => q.Timeline).ToListAsync();
            }
        }
    }
}
