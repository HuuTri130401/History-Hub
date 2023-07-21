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
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public DetailsModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

      public Quiz Quiz { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Quizzes == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.FirstOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }
            else 
            {
                Quiz = quiz;
            }
            return Page();
        }
    }
}
